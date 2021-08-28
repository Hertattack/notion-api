using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NotionApi.Rest.Response.Objects;
using NotionApi.Rest.Response.Page.Properties;
using NotionApi.Rest.Response.Reference;
using Util;

namespace NotionApi.Rest.Response.Page
{
    public class PageObject : NotionObject
    {
        [JsonProperty(PropertyName = "archived")]
        public bool Archived { get; set; }

        [JsonProperty("parent")] public ParentReference Parent { get; set; }

        [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty(PropertyName = "properties")]
        public IDictionary<string, NotionPropertyValue> Properties { get; set; } = new Dictionary<string, NotionPropertyValue>();

        [JsonIgnore] public Option<NotionObject> Container { get; set; }

        [JsonIgnore]
        public string Title
        {
            get
            {
                var titleProperty = Properties.Values.OfType<TitlePropertyValue>().FirstOrDefault();

                if (titleProperty?.Title.HasValue == true)
                    return string.Join(" ", titleProperty.Title.Value.Select(t => t.PlainText));

                return Id;
            }
        }
    }
}