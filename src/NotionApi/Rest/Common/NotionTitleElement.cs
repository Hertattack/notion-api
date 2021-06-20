using Newtonsoft.Json;
using NotionApi.Rest.Common.Blocks;

namespace NotionApi.Rest.Common
{
    public class NotionTitleElement
    {
        [JsonProperty(PropertyName = "annotation")]
        public NotionAnnotation Annotation { get; set; }

        [JsonProperty(PropertyName = "plain_text")]
        public string PlainText { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        [JsonProperty(PropertyName = "text")]
        public NotionTextBlock Text { get; set; }
    }
}