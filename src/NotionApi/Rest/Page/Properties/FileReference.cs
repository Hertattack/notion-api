using Newtonsoft.Json;

namespace NotionApi.Rest.Properties
{
    public class FileReference
    {
        [JsonProperty("name")] public string Name { get; set; }
    }
}