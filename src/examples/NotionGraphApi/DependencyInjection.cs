using NotionApi;
using NotionGraphDatabase.Interface;
using RestUtil;

namespace NotionGraphApi;

public static class DependencyInjection
{
    public static void Configure(WebApplicationBuilder builder)
    {
        var serviceCollection = builder.Services;
        var configuration = builder.Configuration;

        serviceCollection.Configure<NotionGraphApiOptions>(
            o => configuration.GetSection(nameof(NotionGraphApi)).Bind(o));
        serviceCollection.Configure<NotionClientOptions>(o => configuration.GetSection(nameof(NotionClient)).Bind(o));
        serviceCollection.Configure<RestClientOptions>(o => configuration.GetSection(nameof(RestClient)).Bind(o));

        serviceCollection.AddTransient<IRestClient, RestClient>();

        ServiceConfigurator.Configure(serviceCollection);

        serviceCollection.AddTransient<INotionClient, NotionClient>();

        serviceCollection.AddTransient<IMetamodelStore, NotionGraphDatabaseModelStore>();

        NotionGraphDatabase.DependencyInjection.Configure(serviceCollection);
    }
}