using Newtonsoft.Json;

namespace NotionApi.Rest.Page.Properties
{
    public class FileReference
    {
        [JsonProperty("name")] public string Name { get; set; }
    }
}