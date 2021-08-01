using Newtonsoft.Json;

namespace NotionApi.Rest.Objects
{
    public class LinkObject : BasicNotionObject
    {
        [JsonProperty("url")] public string Url { get; set; }
    }
}