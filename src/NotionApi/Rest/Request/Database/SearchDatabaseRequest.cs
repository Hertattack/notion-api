using NotionApi.Rest.Response;
using NotionApi.Rest.Response.Objects;
using RestUtil.Mapping;
using RestUtil.Request;
using RestUtil.Request.Attributes;

namespace NotionApi.Rest.Request.Database
{
    [Request(Path = "/databases/{DatabaseId}/query", Method = HttpMethod.Post)]
    public class SearchDatabaseRequest : IPaginatedNotionRequest<PaginatedResponse<NotionObject>>
    {
        [Parameter(Type = ParameterType.Path)] public string DatabaseId { get; set; }

        [Parameter(Type = ParameterType.Body, Strategy = typeof(ToJsonDocumentStrategy))]
        public SearchDatabaseParameters Parameters { get; } = new SearchDatabaseParameters();

        public void SetStartCursor(string value)
        {
            Parameters.StartCursor = value;
        }
    }
}