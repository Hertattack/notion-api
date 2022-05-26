using Newtonsoft.Json;

namespace NotionApi.Rest.Response.Page.Properties.Formula;

public class FormulaValue
{
    [JsonProperty("type")] public string Type { get; set; }
}