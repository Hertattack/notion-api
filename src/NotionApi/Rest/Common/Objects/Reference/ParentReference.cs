using Newtonsoft.Json;

namespace NotionApi.Rest.Common.Objects.Reference
{
    public class ParentReference
    {
        [JsonProperty("type")] public string Type { get; set; }
    }
}