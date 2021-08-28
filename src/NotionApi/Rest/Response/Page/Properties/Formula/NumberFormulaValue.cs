using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Response.Page.Properties.Formula
{
    public class NumberFormulaValue : FormulaValue
    {
        [JsonProperty("number")] public Option<string> Value { get; set; }
    }
}