using RestUtil.Mapping;
using RestUtil.Request;
using RestUtil.Request.Attributes;

namespace NotionApi.Rest.Search
{
    [Request(Path = "/search", Method = HttpMethod.Post)]
    public class SearchRequest : INotionRequest<SearchResults>
    {
        [Parameter(Type = ParameterType.Body, Strategy = typeof(ToJsonDocumentStrategy))]
        public SearchParameters Parameters { get; } = new SearchParameters();
    }
}