using System;
using RestUtil.Request.Attributes;

namespace NotionApi.Rest.Request.Parameter;

public class OnOrAfterDateTimeFilter : TimeFilter
{
    public OnOrAfterDateTimeFilter(DateTime dateTime)
    {
        Value = dateTime;
    }

    [Mapping("on_or_after")] public DateTime Value { get; set; }
}