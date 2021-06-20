using System.Collections.Generic;
using Newtonsoft.Json;
using NotionApi.Rest.Common.Properties;
using NotionApi.Util;

namespace NotionApi.Rest.Common
{
    public class NotionObject
    {
        [JsonProperty(PropertyName = "created_time")]
        public string CreatedTime { get; set; }

        [JsonProperty(PropertyName = "last_edited_time")]
        public string LastEditedTime { get; set; }

        [JsonProperty(PropertyName = "object")]
        public string ObjectType { get; set; }

        [JsonProperty(PropertyName = "id")] public string Id { get; set; }

        [JsonProperty(PropertyName = "archived")]
        public bool Archived { get; set; }

        [JsonProperty(PropertyName = "properties")]
        public IDictionary<string, NotionProperty> Properties { get; set; } = new Dictionary<string, NotionProperty>();

        [JsonProperty(PropertyName = "title")] public IList<NotionTitleElement> Title { get; set; } = new List<NotionTitleElement>();
    }
}