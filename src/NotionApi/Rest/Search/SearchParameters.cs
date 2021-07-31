using NotionApi.Rest.Common;
using NotionApi.Rest.Parameter;
using RestUtil.Request.Attributes;
using Util;

namespace NotionApi.Rest.Search
{
    public class SearchParameters : PaginatedRequest
    {
        [Mapping("query")] public Option<string> Query { get; set; }

        public Option<SortParameter> Sort { get; set; }

        public Option<FilterParameter> Filter { get; set; }
    }
}