using Newtonsoft.Json;

namespace NotionApi.Rest.Response.Database.Properties
{
    public class SelectPropertyConfiguration : NotionPropertyConfiguration
    {
        [JsonProperty("select")] public SelectConfiguration Configuration { get; set; }
    }
}