using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Common.Objects.Text
{
    public class TextObject
    {
        [JsonProperty("content")] public string Content { get; set; }

        [JsonProperty("link")] public Option<LinkObject> Link { get; set; }
    }
}