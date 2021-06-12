using NotionApi.Request.Attributes;

namespace NotionApi.Rest.Parameter
{
    public enum SortTimestamp
    {
        None,

        [Mapping("last_edit_time")] LastEditedTime
    }
}