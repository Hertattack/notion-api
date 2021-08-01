using Newtonsoft.Json;

namespace NotionApi.Rest.Database.Properties
{
    public class SelectPropertyConfiguration : NotionPropertyConfiguration
    {
        [JsonProperty("select")] public SelectConfiguration Configuration { get; set; }
    }
}