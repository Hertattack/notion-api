using Newtonsoft.Json;

namespace NotionApi.Rest.Common.Blocks
{
    public class NotionTextBlock
    {
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "link")] 
        public string Link { get; set; }
    }
}