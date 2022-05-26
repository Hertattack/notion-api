using Newtonsoft.Json;

namespace NotionApi.Rest.Response.Text;

public class RichTextObject
{
    [JsonProperty("plain_text")] public string PlainText { get; set; }
    [JsonProperty("href")] public string Href { get; set; }
    [JsonProperty("annotations")] public Annotations Annotations { get; set; }
    [JsonProperty("type")] public string Type { get; set; }
}