using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Page.Properties.Formula
{
    public class StringFormulaValue : FormulaValue
    {
        [JsonProperty("string")] public Option<string> Value { get; set; }
    }
}