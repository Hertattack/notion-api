using NotionApi.Request.Attributes;
using NotionApi.Util;

namespace NotionApi.Request
{
    public interface IPaginatedRequest
    {
        [Mapping("start_cursor")]
        public Option<string> StartCursor { get; set; }
        
        [Mapping("page_size")]
        public int PageSize { get; set; }
    }
}