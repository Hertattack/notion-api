using Newtonsoft.Json;

namespace NotionApi.Rest.Response.Objects;

public class BasicNotionObject
{
    [JsonProperty("type")] public string Type { get; set; }
}