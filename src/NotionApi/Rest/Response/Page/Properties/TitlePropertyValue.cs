using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NotionApi.Rest.Response.Text;
using Util;

namespace NotionApi.Rest.Response.Page.Properties;

public class TitlePropertyValue : NotionPropertyValue
{
    [JsonProperty(PropertyName = "title")]
    public Option<IList<RichTextObject>> Title { get; set; } = new List<RichTextObject>();

    public override string ToString()
    {
        return Title.HasValue ? string.Join(" ", Title.Value.Select(r => r.PlainText)) : "";
    }
}