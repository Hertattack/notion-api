using Newtonsoft.Json;

namespace NotionApi.Rest.Common.Properties
{
    public class ManyToOneRelationshipProperty : NotionProperty
    {
        [JsonProperty(PropertyName = "database_id")]
        public string DatabaseId { get; set; }

        [JsonProperty(PropertyName = "synced_property_name")]
        public string SyncedPropertyName { get; set; }

        [JsonProperty(PropertyName = "synced_property_id")]
        public string SyncedPropertyId { get; set; }
    }
}