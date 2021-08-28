using Newtonsoft.Json;

namespace NotionApi.Rest.Response.Text
{
    public class RichTextMentionObject : RichTextObject
    {
        [JsonProperty("mention")] public Mention.Mention Mention { get; set; }
    }
}