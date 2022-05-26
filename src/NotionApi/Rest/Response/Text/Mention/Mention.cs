using Newtonsoft.Json;

namespace NotionApi.Rest.Response.Text.Mention;

public class Mention
{
    [JsonProperty("type")] public string Type { get; set; }
}