using Newtonsoft.Json;

namespace NotionApi.Rest.Common.Properties
{
    public class NotionProperty
    {
        [JsonProperty(PropertyName = "id")] public string Id { get; set; }

        [JsonProperty(PropertyName = "type")] public string Type { get; set; }
    }
}