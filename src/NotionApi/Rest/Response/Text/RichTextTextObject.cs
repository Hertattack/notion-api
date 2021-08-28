using Newtonsoft.Json;

namespace NotionApi.Rest.Response.Text
{
    public class RichTextTextObject : RichTextObject
    {
        [JsonProperty("text")] public TextObject Text { get; set; }
    }
}