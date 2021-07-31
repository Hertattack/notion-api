using Newtonsoft.Json;

namespace NotionApi.Rest.Common.Objects
{
    public class BasicNotionObject
    {
        [JsonProperty("type")] public string Type { get; set; }
    }
}