using Newtonsoft.Json;

namespace NotionApi.Rest.Text
{
    public class RichTextEquationObject : RichTextObject
    {
        [JsonProperty("expression")] public string Expression { get; set; }
    }
}