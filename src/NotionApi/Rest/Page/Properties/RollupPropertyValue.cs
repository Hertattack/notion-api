using Newtonsoft.Json;
using NotionApi.Rest.Page.Properties.Rollup;
using Util;

namespace NotionApi.Rest.Page.Properties
{
    public class RollupPropertyValue : NotionPropertyValue
    {
        [JsonProperty("rollup")] public Option<RollupValue> Rollup { get; set; }
    }
}