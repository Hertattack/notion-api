using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Response.Page.Properties.Rollup;

public class NumberRollupValue : RollupValue
{
    [JsonProperty("number")] public Option<string> Value { get; set; }
}