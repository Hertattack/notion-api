using System.Collections.Generic;
using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Common.Properties
{
    public class TitleProperty : NotionProperty
    {
        [JsonProperty(PropertyName = "title")] public Option<IList<NotionTitleElement>> Title { get; set; } = new List<NotionTitleElement>();
    }
}