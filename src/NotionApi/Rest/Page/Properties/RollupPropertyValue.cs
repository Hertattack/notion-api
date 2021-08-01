using Newtonsoft.Json;
using NotionApi.Rest.Properties.Rollup;
using Util;

namespace NotionApi.Rest.Properties
{
    public class RollupPropertyValue : NotionPropertyValue
    {
        [JsonProperty("rollup")] public Option<RollupValue> Rollup { get; set; }
    }
}