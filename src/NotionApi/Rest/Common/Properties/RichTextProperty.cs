using System.Collections.Generic;
using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Common.Properties
{
    public class RichTextProperty : NotionProperty
    {
        [JsonProperty(PropertyName = "rich_text")]
        public Option<IList<object>> RichText { get; set; } = new List<object>();
    }
}