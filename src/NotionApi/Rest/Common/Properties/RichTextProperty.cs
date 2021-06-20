using System.Collections.Generic;
using Newtonsoft.Json;

namespace NotionApi.Rest.Common.Properties
{
    public class RichTextProperty : NotionProperty
    {
        [JsonProperty(PropertyName = "rich_text")]
        public IList<object> RichText { get; set; }
    }
}