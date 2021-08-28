using Newtonsoft.Json;

namespace NotionApi.Rest.Response.Text
{
    public class RichTextEquationObject : RichTextObject
    {
        [JsonProperty("expression")] public string Expression { get; set; }
    }
}