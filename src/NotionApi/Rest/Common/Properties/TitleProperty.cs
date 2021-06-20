using System.Collections.Generic;
using Newtonsoft.Json;

namespace NotionApi.Rest.Common.Properties
{
    public class TitleProperty : NotionProperty
    {
        [JsonProperty(PropertyName = "title")] public IList<NotionTitleElement> Title { get; set; } = new List<NotionTitleElement>();
    }
}