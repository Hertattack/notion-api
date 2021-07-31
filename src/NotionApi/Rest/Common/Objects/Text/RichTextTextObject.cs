using Newtonsoft.Json;

namespace NotionApi.Rest.Common.Objects.Text
{
    public class RichTextTextObject : RichTextObject
    {
        [JsonProperty("text")] public TextObject Text { get; set; }
    }
}