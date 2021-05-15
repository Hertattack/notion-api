using NotionApi.Commands.Search.Attributes;

namespace NotionApi.Commands.Search.BodyParameters
{
    public class SearchFilter
    {
        [Name("property")] public string Property => "object";

        [Name("value")] public string Value { get; set; }
    }
}