using Newtonsoft.Json;

namespace NotionApi.Rest.Response.Page.Properties;

public class FileReference
{
    [JsonProperty("name")] public string Name { get; set; }
}