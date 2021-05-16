using NotionApi.Commands.Attributes;

namespace NotionApi.Commands.BodyParameters
{
    public class SearchFilter
    {
        [Name("property")] public string Property => "object";

        [Name("value")] public string Value { get; set; }
    }
}