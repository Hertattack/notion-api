using RestUtil.Request.Attributes;
using Util;

namespace RestUtil.Request
{
    public interface IPaginatedRequest
    {
        [Mapping("start_cursor")]
        public Option<string> StartCursor { get; set; }
        
        [Mapping("page_size")]
        public int PageSize { get; set; }
    }
}