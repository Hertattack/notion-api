using Newtonsoft.Json;
using NotionApi.Rest.Reference;

namespace NotionApi.Rest.Objects
{
    public class PageObject : NotionObject
    {
        [JsonProperty("parent")] public ParentReference Parent { get; set; }

        [JsonProperty("url")] public string Url { get; set; }
    }
}