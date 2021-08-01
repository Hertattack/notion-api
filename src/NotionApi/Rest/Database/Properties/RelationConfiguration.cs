using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Database.Properties
{
    public class RelationConfiguration
    {
        [JsonProperty("database_id")] public string DatabaseId { get; set; }

        [JsonProperty("synced_property_name")] public Option<string> SyncedPropertyName { get; set; }
        [JsonProperty("synced_property_id")] public Option<string> SyncedPropertyId { get; set; }
    }
}