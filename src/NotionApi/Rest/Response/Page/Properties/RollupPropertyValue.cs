using Newtonsoft.Json;
using NotionApi.Rest.Response.Page.Properties.Rollup;
using Util;

namespace NotionApi.Rest.Response.Page.Properties;

public class RollupPropertyValue : NotionPropertyValue
{
    [JsonProperty("rollup")] public Option<RollupValue> Rollup { get; set; }
}