using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Properties
{
    public class StringFormulaValue : FormulaValue
    {
        [JsonProperty("string")] public Option<string> Value { get; set; }
    }
}