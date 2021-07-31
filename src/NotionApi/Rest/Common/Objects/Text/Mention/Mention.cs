using Newtonsoft.Json;

namespace NotionApi.Rest.Common.Objects.Text.Mention
{
    public class Mention
    {
        [JsonProperty("type")] public string Type { get; set; }
    }
}