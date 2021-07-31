using Newtonsoft.Json;

namespace NotionApi.Rest.Common
{
    public class ParentDatabaseReference : ParentReference
    {
        [JsonProperty("database_id")] public string DatabaseId { get; set; }
    }
}