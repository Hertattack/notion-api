using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Response.Database.Properties
{
    public class FormulaConfiguration
    {
        [JsonProperty("expression")] public Option<string> Expression { get; set; }
    }
}