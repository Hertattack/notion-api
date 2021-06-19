using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NotionApi;
using NotionApi.Rest;
using RestUtil;

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

            var searchRequest = notionClient.CreateRequest<Search>();
            searchRequest.Query = "test";

            var result = await searchRequest.Execute();

            return 0;
        }

        private static IServiceProvider CreateServiceProvider(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (IsDebug)
                configurationBuilder.AddJsonFile("appsettings.Debug.json", optional: false, reloadOnChange: true);

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

            serviceCollection.AddLogging(loggingBuilder =>
            {
                loggingBuilder
                    .AddConfiguration(configuration)
                    .AddConsole();
            });

            serviceCollection.AddTransient<IRestClient, RestClient>();
            ServiceConfigurator.Configure(serviceCollection);

            serviceCollection.AddSingleton<ITokenProvider, TokenProvider>();
            
            serviceCollection.AddTransient<INotionClient, NotionClient>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}