using Newtonsoft.Json;

namespace NotionApi.Rest.Common
{
    public interface IPaginatedResponse
    {
        [JsonProperty(PropertyName = "has_more")]
        bool HasMore { get; set; }

        [JsonProperty(PropertyName = "next_cursor")]
        string NextCursor { get; set; }
    }
}