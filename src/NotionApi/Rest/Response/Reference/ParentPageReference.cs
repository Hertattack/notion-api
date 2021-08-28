using Newtonsoft.Json;

namespace NotionApi.Rest.Response.Reference
{
    public class ParentPageReference : ParentReference
    {
        [JsonProperty("page_id")] public string PageId { get; set; }
    }
}