using RestUtil.Request.Attributes;
using Util;

namespace NotionApi.Rest
{
    public interface IPaginatedRequest
    {
        [Mapping("start_cursor")]
        public Option<string> StartCursor { get; set; }
        
        [Mapping("page_size")]
        public int PageSize { get; set; }
    }
}