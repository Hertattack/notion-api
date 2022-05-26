using System.Collections.Generic;
using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Response.Page.Properties.Rollup;

public class ArrayRollupValue : RollupValue
{
    [JsonProperty("array")] public Option<IList<NotionPropertyValue>> Value { get; set; }
}