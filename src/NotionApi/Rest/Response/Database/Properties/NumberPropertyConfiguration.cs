using Newtonsoft.Json;

namespace NotionApi.Rest.Response.Database.Properties
{
    public class NumberPropertyConfiguration : NotionPropertyConfiguration
    {
        [JsonProperty("number")] public NumberConfiguration Configuration { get; set; }
    }
}