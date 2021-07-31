using Newtonsoft.Json;
using NotionApi.Rest.Common.Objects.Reference;

namespace NotionApi.Rest.Common.Objects
{
    public class PageObject : NotionObject
    {
        [JsonProperty("parent")] public ParentReference Parent { get; set; }

        [JsonProperty("url")] public string Url { get; set; }
    }
}