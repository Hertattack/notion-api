using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NotionApi;
using NotionApi.Rest;
using NotionApi.Rest.Objects;
using NotionApi.Rest.Page;
using NotionApi.Rest.Search;
using RestUtil;
using Util;

namespace NotionVisualizer
{
    internal static class Program
    {
#if DEBUG
        private static bool IsDebug => true;
#else
        private static bool IsDebug => false;
#endif

        private static int Main(string[] args)
        {
            var container = CreateServiceProvider(args);

            var notionClient = container.GetService<INotionClient>() ?? throw new NullReferenceException("Notion client service not available.");

            var searchRequest = new SearchRequest();

            Option<IPaginatedResponse<NotionObject>> response;

            if (args.Length == 1)
                response = notionClient.ReadFromDisk(searchRequest, args[0]).Result;
            else
                response = notionClient.ExecuteRequest(searchRequest).Result;

            if (!response.HasValue)
            {
                Console.WriteLine("No response received.");
                return 0;
            }

            var result = response.Value;
            var cache = notionClient.CreateCache();
            cache.UpdateObjects(result.Results);

            var distinctPropertyTypes = new HashSet<string>();
            foreach (var obj in result.Results)
            {
                if (obj is PageObject page)
                {
                    foreach (var property in page.Properties.Values)
                    {
                        distinctPropertyTypes.Add(property.Type);
                    }
                }
            }

            foreach (var ptype in distinctPropertyTypes)
            {
                Console.WriteLine(ptype);
            }

            return 0;
        }

        private static IServiceProvider CreateServiceProvider(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (IsDebug)
                configurationBuilder.AddJsonFile("appsettings.Debug.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = configurationBuilder
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            return CreateServiceContainer(configuration);
        }

        private static IServiceProvider CreateServiceContainer(IConfiguration configuration)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.Configure<NotionClientOptions>(o => configuration.GetSection(nameof(NotionClient)).Bind(o));
            serviceCollection.Configure<RestClientOptions>(o => configuration.GetSection(nameof(RestClient)).Bind(o));

            serviceCollection.AddLogging(loggingBuilder =>
            {
                loggingBuilder
                    .AddConsole()
                    .AddConfiguration(configuration.GetSection("Logging"));
            });

            serviceCollection.AddTransient<IRestClient, RestClient>();

            ServiceConfigurator.Configure(serviceCollection);

            serviceCollection.AddTransient<INotionClient, NotionClient>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}