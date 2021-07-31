using Newtonsoft.Json;

namespace NotionApi.Rest.Common
{
    public class PageObject : NotionObject
    {
        [JsonProperty("parent")] public ParentReference Parent { get; set; }

        [JsonProperty("url")] public string Url { get; set; }
    }
}