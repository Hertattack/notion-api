using Newtonsoft.Json;

namespace NotionApi.Rest.Response.Database.Properties;

public class NumberConfiguration
{
    [JsonProperty("format")] public string Format { get; set; }
}