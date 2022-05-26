using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Response.Database.Properties;

public class RelationConfiguration
{
    [JsonIgnore] public Option<DatabaseObject> SyncedDatabase { get; set; }

    [JsonProperty("database_id")] public string DatabaseId { get; set; }

    [JsonProperty("synced_property_name")] public Option<string> SyncedPropertyName { get; set; }
    [JsonProperty("synced_property_id")] public Option<string> SyncedPropertyId { get; set; }
}