using System.Collections.Generic;
using Newtonsoft.Json;

namespace NotionApi.Rest.Response
{
    public class PaginatedResponse<TResultType> : IPaginatedResponse<TResultType>
    {
        [JsonProperty("has_more")] public bool HasMore { get; set; }

        [JsonProperty("next_cursor")] public string NextCursor { get; set; }

        [JsonProperty("object")] public string ObjectType { get; set; }

        [JsonProperty(PropertyName = "results")]
        public IList<TResultType> Results { get; set; }
    }
}