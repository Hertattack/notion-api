using RestUtil.Request.Attributes;

namespace NotionApi.Rest.Request.Parameter;

public class DatabaseLastEditedTimestampFilter : DatabaseFilter
{
    [Mapping("timestamp")] public string Timestamp => "last_edited_time";
    [Mapping("last_edited_time")] public TimeFilter LastEditedTimeFilter { get; set; }
}