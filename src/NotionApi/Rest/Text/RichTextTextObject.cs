using Newtonsoft.Json;

namespace NotionApi.Rest.Text
{
    public class RichTextTextObject : RichTextObject
    {
        [JsonProperty("text")] public TextObject Text { get; set; }
    }
}