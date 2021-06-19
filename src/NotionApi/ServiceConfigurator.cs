using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestUtil.Request;

namespace NotionApi
{
    public static class ServiceConfigurator
    {
        public static void Configure(HostBuilderContext builderContext, IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMapper, Mapper>();
            serviceCollection.AddTransient<IRequestBuilder, RequestBuilder>();
            serviceCollection.AddTransient<INotionClient, NotionClient>();
        }
    }
}