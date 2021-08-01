using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NotionApi;
using NotionApi.Rest.Objects;
using NotionApi.Rest.Search;
using RestUtil;
using Util.Visitor;

namespace NotionVisualizer
{
    internal static class Program
    {
#if DEBUG
        private static bool IsDebug => true;
#else
        private static bool IsDebug => false;
#endif

        private static async Task<int> Main(string[] args)
        {
            var container = CreateServiceProvider(args);

            var notionClient = container.GetService<INotionClient>() ?? throw new NullReferenceException("Notion client service not available.");

            var searchRequest = new SearchRequest();

            var response = await notionClient.ExecuteRequest(searchRequest);

            if (!response.HasValue)
            {
                Console.WriteLine("No response received.");
                return 0;
            }

            var result = response.Value;

            var visitor = new ObjectVisitor();
            visitor.RegisterAction<NotionObject>(HandleObject);
            
            var distinctPropertyTypes = new HashSet<string>();
            foreach (var page in result.Results)
            {
                foreach (var property in page.Properties.Values)
                {
                    distinctPropertyTypes.Add(property.Type);
                }
            }

            foreach (var ptype in distinctPropertyTypes)
            {
                Console.WriteLine(ptype);
            }

            return 0;
        }

        private static void HandleObject(VisitorPath path, NotionObject visitedNode)
        {
            
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