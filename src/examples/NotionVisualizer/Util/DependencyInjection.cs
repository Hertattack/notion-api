using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NotionApi;
using NotionVisualizer.Generator;
using RestUtil;

namespace NotionVisualizer.Util
{
    public static class DependencyInjection
    {
#if DEBUG
        private static bool IsDebug => true;
#else
        private static bool IsDebug => false;
#endif

        public static IServiceProvider CreateServiceProvider()
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (IsDebug)
                configurationBuilder.AddJsonFile("appsettings.Debug.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = configurationBuilder
                .AddEnvironmentVariables()
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

            serviceCollection.AddTransient<CytoscapeGenerator>();
            serviceCollection.AddTransient<Visualizer>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}