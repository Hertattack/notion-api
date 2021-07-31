using Newtonsoft.Json;
using NotionApi.Rest.Common.Objects.Text.Mention;

namespace NotionApi.Rest.Common.Objects.Text
{
    public class RichTextMentionObject : RichTextObject
    {
        [JsonProperty("mention")] public Mention.Mention Mention { get; set; }
    }
}