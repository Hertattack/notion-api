using Newtonsoft.Json;

namespace NotionApi.Rest.Response.Database.Properties
{
    public class MultiSelectPropertyConfiguration : NotionPropertyConfiguration
    {
        [JsonProperty("multi_select")] public SelectConfiguration Configuration { get; set; }
    }
}