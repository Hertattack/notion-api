using Newtonsoft.Json;

namespace NotionApi.Rest.Text.Mention
{
    public class Mention
    {
        [JsonProperty("type")] public string Type { get; set; }
    }
}