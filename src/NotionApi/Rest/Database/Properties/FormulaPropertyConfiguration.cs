using Newtonsoft.Json;

namespace NotionApi.Rest.Database.Properties
{
    public class FormulaPropertyConfiguration : NotionPropertyConfiguration
    {
        [JsonProperty("formula")] public FormulaConfiguration Configuration { get; set; }
    }
}