using Newtonsoft.Json;

namespace NotionApi.Rest.Response.Reference
{
    public class ParentReference
    {
        [JsonProperty("type")] public string Type { get; set; }
    }
}