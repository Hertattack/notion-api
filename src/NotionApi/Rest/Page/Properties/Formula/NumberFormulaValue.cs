using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Properties
{
    public class NumberFormulaValue : FormulaValue
    {
        [JsonProperty("number")] public Option<string> Value { get; set; }
    }
}