using RestUtil.Request.Attributes;
using Util;

namespace NotionApi.Rest.Common
{
    public interface IPaginatedRequest
    {
        [Mapping("start_cursor")]
        public Option<string> StartCursor { get; set; }
        
        [Mapping("page_size")]
        public int PageSize { get; set; }
    }
}