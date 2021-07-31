using Newtonsoft.Json;

namespace NotionApi.Rest.Common.Properties
{
    public class RelationReferenceProperty
    {
        [JsonProperty(PropertyName = "id")] public string Id { get; set; }
    }
}