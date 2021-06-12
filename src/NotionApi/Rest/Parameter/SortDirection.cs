using NotionApi.Request.Attributes;

namespace NotionApi.Rest.Parameter
{
    public enum SortDirection
    {
        [Mapping("ascending")] Ascending,

        [Mapping("descending")] Descending
    }
}