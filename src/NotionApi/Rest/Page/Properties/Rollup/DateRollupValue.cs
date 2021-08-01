﻿using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Page.Properties.Rollup
{
    public class DateRollupValue : RollupValue
    {
        [JsonProperty("date")] public Option<DateValue> Value { get; set; }
    }
}