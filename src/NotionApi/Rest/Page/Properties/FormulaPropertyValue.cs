using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Properties
{
    public class FormulaPropertyValue : NotionPropertyValue
    {
        [JsonProperty("formula")] public Option<FormulaValue> Formula { get; set; }
    }
}