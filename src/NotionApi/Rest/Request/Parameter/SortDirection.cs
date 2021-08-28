using RestUtil.Request.Attributes;

namespace NotionApi.Rest.Request.Parameter
{
    public enum SortDirection
    {
        [Mapping("ascending")] Ascending,

        [Mapping("descending")] Descending
    }
}