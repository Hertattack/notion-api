using System.Collections.Generic;
using Newtonsoft.Json;
using NotionApi.Rest.Properties;
using NotionApi.Rest.Text;

namespace NotionApi.Rest.Objects
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
        public IDictionary<string, NotionPropertyValue> Properties { get; set; } = new Dictionary<string, NotionPropertyValue>();

        [JsonProperty(PropertyName = "title")] public IList<RichTextObject> Title { get; set; } = new List<RichTextObject>();
    }
}