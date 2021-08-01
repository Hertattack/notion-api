using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Page.Properties.Rollup
{
    public class NumberRollupValue : RollupValue
    {
        [JsonProperty("number")] public Option<string> Value { get; set; }
    }
}