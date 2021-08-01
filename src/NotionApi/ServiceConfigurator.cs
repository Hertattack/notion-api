using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NotionApi.Util;
using RestUtil.Request;

namespace NotionApi
{
    public static class ServiceConfigurator
    {
        public static void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMapper, Mapper>();
            serviceCollection.AddTransient<IRequestBuilder, RequestBuilder>();

            serviceCollection.AddTransient<JsonConverter, OptionConverter>();
            serviceCollection.AddTransient<JsonConverter, NotionPagePropertyConverter>();
            serviceCollection.AddTransient<JsonConverter, NotionObjectConverter>();
            serviceCollection.AddTransient<JsonConverter, NotionParentReferenceJsonConverter>();
            serviceCollection.AddTransient<JsonConverter, NotionRichTextConverter>();
            serviceCollection.AddTransient<JsonConverter, BasicNotionObjectConverter>();
            serviceCollection.AddTransient<JsonConverter, NotionMentionConverter>();
            serviceCollection.AddTransient<JsonConverter, NotionFormulaValueConverter>();
            serviceCollection.AddTransient<JsonConverter, NotionRollupValueConverter>();
            serviceCollection.AddTransient<OptionConverter>();
        }
    }
}