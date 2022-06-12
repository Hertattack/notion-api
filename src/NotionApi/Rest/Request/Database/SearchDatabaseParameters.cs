using NotionApi.Rest.Request.Parameter;
using RestUtil.Request.Attributes;
using Util;

namespace NotionApi.Rest.Request.Database;

public class SearchDatabaseParameters : PaginatedRequest
{
    [Mapping("filter")] public Option<DatabaseFilter> Filter { get; set; } = Option.None;
}