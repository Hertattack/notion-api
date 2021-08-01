using Newtonsoft.Json;

namespace NotionApi.Rest.Database.Properties
{
    public class RollupPropertyConfiguration : NotionPropertyConfiguration
    {
        [JsonProperty("rollup")] public RollupConfiguration Configuration { get; set; }
    }
}