using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Database.Properties
{
    public class FormulaConfiguration
    {
        [JsonProperty("expression")] public Option<string> Expression { get; set; }
    }
}