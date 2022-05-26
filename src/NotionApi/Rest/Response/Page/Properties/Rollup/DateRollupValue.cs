using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Response.Page.Properties.Rollup;

public class DateRollupValue : RollupValue
{
    [JsonProperty("date")] public Option<DateValue> Value { get; set; }
}