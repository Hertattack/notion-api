using Newtonsoft.Json;
using NotionApi.Rest.Page.Properties.Formula;
using Util;

namespace NotionApi.Rest.Page.Properties
{
    public class FormulaPropertyValue : NotionPropertyValue
    {
        [JsonProperty("formula")] public Option<FormulaValue> Formula { get; set; }
    }
}