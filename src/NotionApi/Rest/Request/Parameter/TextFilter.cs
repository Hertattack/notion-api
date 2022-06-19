using RestUtil.Request.Attributes;

namespace NotionApi.Rest.Request.Parameter;

public class TextFilter
{
    [Mapping("equals")] public string EqualTo { get; set; }
}