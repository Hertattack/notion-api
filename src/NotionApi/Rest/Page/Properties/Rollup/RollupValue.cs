using Newtonsoft.Json;

namespace NotionApi.Rest.Page.Properties.Rollup
{
    public class RollupValue
    {
        [JsonProperty("type")] public string Type { get; set; }
    }
}