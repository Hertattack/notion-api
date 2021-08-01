using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Properties
{
    public class BooleanFormulaValue : FormulaValue
    {
        [JsonProperty("boolean")] public Option<bool> Value { get; set; }
    }
}