using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Response.Page.Properties.Formula;

public class BooleanFormulaValue : FormulaValue
{
    [JsonProperty("boolean")] public Option<bool> Value { get; set; }
}