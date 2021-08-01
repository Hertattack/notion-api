using Newtonsoft.Json;

namespace NotionApi.Rest.Properties
{
    public class RelationReferenceProperty
    {
        [JsonProperty(PropertyName = "id")] public string Id { get; set; }
    }
}