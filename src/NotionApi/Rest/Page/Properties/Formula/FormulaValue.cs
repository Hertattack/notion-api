using Newtonsoft.Json;

namespace NotionApi.Rest.Page.Properties.Formula
{
    public class FormulaValue
    {
        [JsonProperty("type")] public string Type { get; set; }
    }
}