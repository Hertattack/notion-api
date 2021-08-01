using Newtonsoft.Json;

namespace NotionApi.Rest.Reference
{
    public class ParentDatabaseReference : ParentReference
    {
        [JsonProperty("database_id")] public string DatabaseId { get; set; }
    }
}