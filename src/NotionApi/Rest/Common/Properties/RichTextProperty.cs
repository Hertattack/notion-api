using System.Collections.Generic;
using Newtonsoft.Json;
using NotionApi.Rest.Common.Objects.Text;
using Util;

namespace NotionApi.Rest.Common.Properties
{
    public class RichTextProperty : NotionProperty
    {
        [JsonProperty(PropertyName = "rich_text")]
        public Option<IList<RichTextObject>> RichText { get; set; } = new List<RichTextObject>();
    }
}