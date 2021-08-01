using System.Collections.Generic;
using Newtonsoft.Json;
using NotionApi.Rest.Objects;
using NotionApi.Rest.Page.Properties;
using NotionApi.Rest.Reference;

namespace NotionApi.Rest.Page
{
    public class PageObject : NotionObject
    {
        [JsonProperty(PropertyName = "archived")]
        public bool Archived { get; set; }

        [JsonProperty("parent")] public ParentReference Parent { get; set; }

        [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty(PropertyName = "properties")]
        public IDictionary<string, NotionPropertyValue> Properties { get; set; } = new Dictionary<string, NotionPropertyValue>();
    }
}