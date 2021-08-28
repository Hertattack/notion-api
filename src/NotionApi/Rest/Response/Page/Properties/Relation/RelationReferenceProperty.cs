using Newtonsoft.Json;

namespace NotionApi.Rest.Response.Page.Properties.Relation
{
    public class RelationReferenceProperty
    {
        [JsonProperty(PropertyName = "id")] public string Id { get; set; }
    }
}