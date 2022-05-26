using Newtonsoft.Json;
using NotionApi.Rest.Response.Page.Properties.Formula;
using Util;

namespace NotionApi.Rest.Response.Page.Properties;

public class FormulaPropertyValue : NotionPropertyValue
{
    [JsonProperty("formula")] public Option<FormulaValue> Formula { get; set; }
}