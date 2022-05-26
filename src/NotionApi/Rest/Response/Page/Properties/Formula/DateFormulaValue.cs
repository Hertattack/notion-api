using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Response.Page.Properties.Formula;

public class DateFormulaValue : FormulaValue
{
    [JsonProperty("date")] public Option<DateValue> Value { get; set; }
}