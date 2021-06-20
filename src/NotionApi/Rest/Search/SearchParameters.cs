using NotionApi.Rest.Common;
using NotionApi.Rest.Parameter;
using RestUtil.Request.Attributes;
using Util;

namespace NotionApi.Rest.Search
{
    public class SearchParameters : IPaginatedRequest
    {
        [Mapping("query")] public Option<string> Query { get; set; }

        public Option<SortParameter> Sort { get; set; }

        public Option<FilterParameter> Filter { get; set; }

        private PaginationOptions Pagination { get; } = new PaginationOptions();

        public Option<string> StartCursor
        {
            get => Pagination.StartCursor;
            set => Pagination.StartCursor = value;
        }

        public int PageSize
        {
            get => Pagination.PageSize;
            set => Pagination.PageSize = value;
        }
    }
}