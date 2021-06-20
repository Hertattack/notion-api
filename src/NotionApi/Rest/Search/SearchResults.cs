using System.Collections.Generic;
using NotionApi.Rest.Common;

namespace NotionApi.Rest.Search
{
    public class SearchResults : IPaginatedResponse, IListResponse<NotionObject>
    {
        public bool HasMore { get; set; }
        public string NextCursor { get; set; }
        public string ObjectType { get; set; }
        public IList<NotionObject> Results { get; set; } = new List<NotionObject>();
    }
}