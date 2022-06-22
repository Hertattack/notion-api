using RestUtil.Request.Attributes;
using Util;

namespace NotionApi.Rest.Request.Parameter;

public class TextFilter
{
    [Mapping("equals")] public Option<string> EqualTo { get; set; }

    [Mapping("does_not_equal")] public Option<string> DoesNotEqual { get; set; }

    [Mapping("contains")] public Option<string> Contains { get; set; }

    [Mapping("does_not_contain")] public Option<string> DoesNotContain { get; set; }
}