using Newtonsoft.Json;

namespace NotionApi.Rest.Common
{
    public class ParentReference
    {
        [JsonProperty("type")] public string Type { get; set; }
    }
}