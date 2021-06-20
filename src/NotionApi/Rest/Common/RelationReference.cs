using Newtonsoft.Json;

namespace NotionApi.Rest.Common
{
    public class RelationReference
    {
        [JsonProperty(PropertyName = "id")] public string Id { get; set; }
    }
}