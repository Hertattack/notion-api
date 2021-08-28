using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NotionApi;
using NotionVisualizer.Generator;
using NotionVisualizer.Generator.Cytoscape;
using NotionVisualizer.Generator.Excel;
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

            if (IsDebug)
                configurationBuilder.AddJsonFile("appsettings.Debug.json", optional: true, reloadOnChange: true);
            else
                configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = configurationBuilder
                .AddEnvironmentVariables()
                .Build();

            return CreateServiceContainer(configuration);
        }

        private static IServiceProvider CreateServiceContainer(IConfiguration configuration)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.Configure<NotionVisualizerOptions>(o => configuration.GetSection(nameof(NotionVisualizer)).Bind(o));
            serviceCollection.Configure<CytoscapeGeneratorOptions>(o => configuration.GetSection(nameof(CytoscapeGenerator)).Bind(o));
            serviceCollection.Configure<ExcelGeneratorOptions>(o => configuration.GetSection(nameof(ExcelGenerator)).Bind(o));
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

            serviceCollection.AddTransient<IGenerator, CytoscapeGenerator>();
            serviceCollection.AddTransient<IGenerator, ExcelGenerator>();

            serviceCollection.AddTransient<Visualizer>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}