using RestUtil.Request.Attributes;

namespace NotionApi.Rest.Request.Parameter;

public class EqualsTimeFilter : TimeFilter
{
    public EqualsTimeFilter(string ts)
    {
        Value = ts;
    }

    [Mapping("equals")] public string Value { get; set; }
}