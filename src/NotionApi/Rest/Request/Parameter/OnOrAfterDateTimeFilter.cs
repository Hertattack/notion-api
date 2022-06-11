using RestUtil.Request.Attributes;

namespace NotionApi.Rest.Request.Parameter;

public class OnOrAfterDateTimeFilter : TimeFilter
{
    public OnOrAfterDateTimeFilter(string ts)
    {
        Value = ts;
    }

    [Mapping("on_or_after")] public string Value { get; set; }
}