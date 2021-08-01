using System.Collections.Generic;
using Newtonsoft.Json;
using NotionApi.Rest.Text;
using Util;

namespace NotionApi.Rest.Properties
{
    public class TitlePropertyValue : NotionPropertyValue
    {
        [JsonProperty(PropertyName = "title")] public Option<IList<RichTextObject>> Title { get; set; } = new List<RichTextObject>();
    }
}