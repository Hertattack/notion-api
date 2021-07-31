using Newtonsoft.Json;

namespace NotionApi.Rest.Common.Objects.Reference
{
    public class ParentDatabaseReference : ParentReference
    {
        [JsonProperty("database_id")] public string DatabaseId { get; set; }
    }
}