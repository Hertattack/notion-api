using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NotionApi
{
    public static class ServiceConfigurator
    {
        public static void Configure(HostBuilderContext builderContext, IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<INotionClient, NotionClient>();
        }
    }
}