using Newtonsoft.Json;

namespace NotionApi.Rest.Database.Properties
{
    public class RelationPropertyConfiguration : NotionPropertyConfiguration
    {
        [JsonProperty("relation")] public RelationConfiguration Configuration { get; set; }
    }
}