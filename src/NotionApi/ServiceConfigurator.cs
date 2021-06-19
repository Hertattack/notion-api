using Microsoft.Extensions.DependencyInjection;
using RestUtil.Request;

namespace NotionApi
{
    public static class ServiceConfigurator
    {
        public static void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMapper, Mapper>();
            serviceCollection.AddTransient<IRequestBuilder, RequestBuilder>();
        }
    }
}