using System.Collections.Generic;
using Newtonsoft.Json;
using NotionApi.Rest.Response.Text;
using Util;

namespace NotionApi.Rest.Response.Page.Properties;

public class RichTextPropertyValue : NotionPropertyValue
{
    [JsonProperty(PropertyName = "rich_text")]
    public Option<IList<RichTextObject>> RichText { get; set; } = new List<RichTextObject>();
}