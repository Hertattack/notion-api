using Newtonsoft.Json;

namespace NotionApi.Rest.Response.Page.Properties.Rollup;

public class RollupValue
{
    [JsonProperty("type")] public string Type { get; set; }
}