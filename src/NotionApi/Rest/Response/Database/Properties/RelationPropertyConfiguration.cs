using Newtonsoft.Json;

namespace NotionApi.Rest.Response.Database.Properties
{
    public class RelationPropertyConfiguration : NotionPropertyConfiguration
    {
        [JsonProperty("relation")] public RelationConfiguration Configuration { get; set; }
    }
}