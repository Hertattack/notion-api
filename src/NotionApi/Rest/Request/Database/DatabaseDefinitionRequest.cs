using NotionApi.Rest.Response.Database;
using RestUtil.Request;
using RestUtil.Request.Attributes;

namespace NotionApi.Rest.Request.Database
{
    [Request(Path = "/databases/{DatabaseId}", Method = HttpMethod.Get)]
    public class DatabaseDefinitionRequest : INotionRequest<DatabaseObject>
    {
        [Parameter(Type = ParameterType.Path)] public string DatabaseId { get; set; }
    }
}