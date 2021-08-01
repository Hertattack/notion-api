using Newtonsoft.Json;

namespace NotionApi.Rest.Reference
{
    public class ParentReference
    {
        [JsonProperty("type")] public string Type { get; set; }
    }
}