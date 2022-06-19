using RestUtil.Request.Attributes;

namespace NotionApi.Rest.Request.Parameter;

public class RichTextPropertyFilter : DatabaseFilter
{
    [Mapping("property")] public string PropertyName { get; set; }
    [Mapping("rich_text")] public TextFilter Filter { get; set; }
}