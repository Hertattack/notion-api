using NotionApi.Rest.Common;
using NotionApi.Rest.Common.Objects;
using RestUtil.Mapping;
using RestUtil.Request;
using RestUtil.Request.Attributes;

namespace NotionApi.Rest.Search
{
    [Request(Path = "/search", Method = HttpMethod.Post)]
    public class SearchRequest : IPaginatedNotionRequest<PaginatedResponse<NotionObject>>
    {
        [Parameter(Type = ParameterType.Body, Strategy = typeof(ToJsonDocumentStrategy))]
        public SearchParameters Parameters { get; } = new SearchParameters();

        public void SetStartCursor(string value)
        {
            Parameters.StartCursor = value;
        }
    }
}