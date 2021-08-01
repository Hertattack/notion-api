using Newtonsoft.Json;

namespace NotionApi.Rest.Page.Properties
{
    public class RelationReferenceProperty
    {
        [JsonProperty(PropertyName = "id")] public string Id { get; set; }
    }
}