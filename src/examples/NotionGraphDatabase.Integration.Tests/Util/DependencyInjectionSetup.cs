using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NotionApi;
using NotionGraphDatabase.Interface;
using NotionGraphDatabase.Metadata;
using RestUtil;

namespace NotionGraphDatabase.Integration.Tests.Util;

public class DependencyInjectionSetup
{
#if DEBUG
    private static bool IsDebug => true;
#else
        private static bool IsDebug => false;
#endif

    public static IServiceProvider CreateServiceProvider()
    {
        var configurationBuilder = new ConfigurationBuilder();

        configurationBuilder.AddJsonFile("appsettings.json", true, true);

        if (IsDebug)
            configurationBuilder.AddJsonFile("appsettings.Debug.json", true, true);

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

        serviceCollection.Configure<Metamodel>(
            o => configuration.GetSection("Model").Bind(o));

        serviceCollection.AddTransient<IMetamodelStore, NotionGraphDatabaseModelStore>();

        DependencyInjection.Configure(serviceCollection);

        return serviceCollection.BuildServiceProvider();
    }
}