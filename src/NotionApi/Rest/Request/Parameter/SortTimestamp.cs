using RestUtil.Request.Attributes;

namespace NotionApi.Rest.Request.Parameter;

public enum SortTimestamp
{
    None,

    [Mapping("last_edit_time")] LastEditedTime
}