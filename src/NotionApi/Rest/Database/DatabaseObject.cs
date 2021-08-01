using System.Collections.Generic;
using Newtonsoft.Json;
using NotionApi.Rest.Database.Properties;
using NotionApi.Rest.Objects;
using NotionApi.Rest.Reference;
using NotionApi.Rest.Text;
using Util;

namespace NotionApi.Rest.Database
{
    public class DatabaseObject : NotionObject
    {
        [JsonProperty("title")] public Option<IList<RichTextObject>> Title { get; set; } = new List<RichTextObject>();

        [JsonProperty("parent")] public Option<ParentReference> Parent { get; set; }

        [JsonProperty("properties")]
        public IDictionary<string, NotionPropertyConfiguration> Properties { get; set; } = new Dictionary<string, NotionPropertyConfiguration>();
    }
}