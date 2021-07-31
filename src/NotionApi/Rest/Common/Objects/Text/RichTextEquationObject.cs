using Newtonsoft.Json;

namespace NotionApi.Rest.Common.Objects.Text
{
    public class RichTextEquationObject : RichTextObject
    {
        [JsonProperty("expression")] public string Expression { get; set; }
    }
}