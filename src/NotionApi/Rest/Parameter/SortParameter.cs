using RestUtil.Mapping;
using RestUtil.Request.Attributes;

namespace NotionApi.Rest.Parameter
{
    [Mapping("sort", Strategy = typeof(ToNestedObjectStrategy))]
    public class SortParameter
    {
        public SortDirection Direction { get; set; } = SortDirection.Ascending;
        public SortTimestamp Timestamp { get; } = SortTimestamp.None;
    }
}