using System.Collections.Generic;
using Newtonsoft.Json;
using NotionApi.Rest.Response.Database.Properties;
using NotionApi.Rest.Response.Objects;
using NotionApi.Rest.Response.Page;
using NotionApi.Rest.Response.Reference;
using NotionApi.Rest.Response.Text;
using Util;

namespace NotionApi.Rest.Response.Database
{
    public class DatabaseObject : NotionObject
    {
        [JsonProperty("title")] public Option<IList<RichTextObject>> Title { get; set; } = new List<RichTextObject>();

        [JsonProperty("parent")] public ParentReference Parent { get; set; }

        [JsonProperty("properties")]
        public IDictionary<string, NotionPropertyConfiguration> Properties { get; set; } = new Dictionary<string, NotionPropertyConfiguration>();

        [JsonIgnore] public Option<PageObject> Container { get; set; }
    }
}