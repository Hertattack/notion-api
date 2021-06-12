using System.Net.Http;
using System.Threading.Tasks;
using NotionApi.Request;
using NotionApi.Request.Attributes;
using NotionApi.Request.Mapping;
using NotionApi.Rest.Parameter;
using NotionApi.Util;

namespace NotionApi.Rest
{
    [Path("v1/search")]
    [Mapping(Strategy = typeof(ToJsonDocumentStrategy))]
    public class Search : IRequest, IPaginatedRequest
    {
        private readonly INotionClient _client;
        private readonly IRequestBuilder _requestBuilder;

        protected Search(INotionClient client, IRequestBuilder requestBuilder)
        {
            _client = client;
            _requestBuilder = requestBuilder;
        }

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

        public async Task<ISearchResults> Execute()
        {
            var request = _requestBuilder.BuildRequest(HttpMethod.Post, this);
            
            return null;
        }
    }
}