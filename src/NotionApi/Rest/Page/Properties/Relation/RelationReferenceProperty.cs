using Newtonsoft.Json;

namespace NotionApi.Rest.Page.Properties.Relation
{
    public class RelationReferenceProperty
    {
        [JsonProperty(PropertyName = "id")] public string Id { get; set; }
    }
}