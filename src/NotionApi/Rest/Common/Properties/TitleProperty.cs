using System.Collections.Generic;
using Newtonsoft.Json;
using NotionApi.Rest.Common.Objects.Text;
using Util;

namespace NotionApi.Rest.Common.Properties
{
    public class TitleProperty : NotionProperty
    {
        [JsonProperty(PropertyName = "title")] public Option<IList<RichTextObject>> Title { get; set; } = new List<RichTextObject>();
    }
}