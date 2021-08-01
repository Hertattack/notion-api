using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Properties
{
    public class DateFormulaValue : FormulaValue
    {
        [JsonProperty("date")] public Option<DateValue> Value { get; set; }
    }
}