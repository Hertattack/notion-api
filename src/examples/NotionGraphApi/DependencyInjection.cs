using Microsoft.Extensions.Options;
using NotionApi;
using NotionGraphDatabase;
using NotionGraphDatabase.Interface;
using RestUtil;

namespace NotionGraphApi
{
    public static class DependencyInjection
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            var serviceCollection = builder.Services;
            var configuration = builder.Configuration;
            
            serviceCollection.Configure<NotionGraphApiOptions>(o => configuration.GetSection(nameof(NotionGraphApi)).Bind(o));
            serviceCollection.Configure<NotionClientOptions>(o => configuration.GetSection(nameof(NotionClient)).Bind(o));
            serviceCollection.Configure<RestClientOptions>(o => configuration.GetSection(nameof(RestClient)).Bind(o));

            serviceCollection.AddTransient<IRestClient, RestClient>();

            ServiceConfigurator.Configure(serviceCollection);

            serviceCollection.AddTransient<INotionClient, NotionClient>();

            serviceCollection.AddSingleton(InitializeDatabase);
        }

        private static IGraphDatabase InitializeDatabase(IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetService<IOptions<NotionGraphApiOptions>>()?.Value;
            if (options is null)
                throw new Exception($"Missing configuration for {nameof(NotionGraphApi)}");

            var notionClient = serviceProvider.GetService<INotionClient>();
            if (notionClient is null)
                throw new Exception("No Notion client available.");
            
            return new GraphDatabase(options.Model, notionClient);
        }
    }
}