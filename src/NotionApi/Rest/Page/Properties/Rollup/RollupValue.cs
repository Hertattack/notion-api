using Newtonsoft.Json;

namespace NotionApi.Rest.Properties.Rollup
{
    public class RollupValue
    {
        [JsonProperty("type")] public string Type { get; set; }
    }
}