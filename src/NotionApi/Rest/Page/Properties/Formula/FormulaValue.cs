using Newtonsoft.Json;

namespace NotionApi.Rest.Properties
{
    public class FormulaValue
    {
        [JsonProperty("type")] public string Type { get; set; }
    }
}