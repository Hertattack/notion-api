using Newtonsoft.Json;
using NotionApi.Rest.Objects;
using Util;

namespace NotionApi.Rest.Text
{
    public class TextObject
    {
        [JsonProperty("content")] public string Content { get; set; }

        [JsonProperty("link")] public Option<LinkObject> Link { get; set; }
    }
}