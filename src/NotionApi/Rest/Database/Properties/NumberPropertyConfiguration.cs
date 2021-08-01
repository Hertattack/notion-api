using Newtonsoft.Json;

namespace NotionApi.Rest.Database.Properties
{
    public class NumberPropertyConfiguration : NotionPropertyConfiguration
    {
        [JsonProperty("number")] public NumberConfiguration Configuration { get; set; }
    }
}