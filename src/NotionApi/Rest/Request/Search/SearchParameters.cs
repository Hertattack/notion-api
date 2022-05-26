using NotionApi.Rest.Request.Parameter;
using RestUtil.Request.Attributes;
using Util;

namespace NotionApi.Rest.Request.Search;

public class SearchParameters : PaginatedRequest
{
    [Mapping("query")] public Option<string> Query { get; set; }

    public Option<SortParameter> Sort { get; set; }

    public Option<FilterParameter> Filter { get; set; }
}