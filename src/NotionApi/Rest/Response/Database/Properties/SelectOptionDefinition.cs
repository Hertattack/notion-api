using Newtonsoft.Json;

namespace NotionApi.Rest.Response.Database.Properties
{
    public class SelectOptionDefinition
    {
        [JsonProperty("select")] public SelectConfiguration Configuration { get; set; }
    }
}