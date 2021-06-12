using NotionApi.Request.Attributes;
using NotionApi.Request.Mapping;

namespace NotionApi.Rest.Parameter
{
    [Mapping("sort", Strategy = typeof(ToNestedObjectStrategy))]
    public class SortParameter
    {
        public SortDirection Direction { get; set; } = SortDirection.Ascending;
        public SortTimestamp Timestamp { get; } = SortTimestamp.None;
    }
}