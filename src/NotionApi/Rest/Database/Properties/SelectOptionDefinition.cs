using Newtonsoft.Json;

namespace NotionApi.Rest.Database.Properties
{
    public class SelectOptionDefinition
    {
        [JsonProperty("select")] public SelectConfiguration Configuration { get; set; }
    }
}