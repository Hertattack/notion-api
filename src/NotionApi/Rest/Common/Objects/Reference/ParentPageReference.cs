using Newtonsoft.Json;

namespace NotionApi.Rest.Common.Objects.Reference
{
    public class ParentPageReference : ParentReference
    {
        [JsonProperty("page_id")] public string PageId { get; set; }
    }
}