using Newtonsoft.Json;

namespace NotionApi.Rest.Text
{
    public class RichTextMentionObject : RichTextObject
    {
        [JsonProperty("mention")] public Mention.Mention Mention { get; set; }
    }
}