using Newtonsoft.Json;

namespace NotionApi.Rest.Page.Properties
{
    public class ManyToOneRelationshipPropertyValue : NotionPropertyValue
    {
        [JsonProperty(PropertyName = "database_id")]
        public string DatabaseId { get; set; }

        [JsonProperty(PropertyName = "synced_property_name")]
        public string SyncedPropertyName { get; set; }

        [JsonProperty(PropertyName = "synced_property_id")]
        public string SyncedPropertyId { get; set; }
    }
}