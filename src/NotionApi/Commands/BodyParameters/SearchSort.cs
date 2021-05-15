using NotionApi.Commands.Search.Attributes;

namespace NotionApi.Commands.Search.BodyParameters
{
    public class SearchSort
    {
        [Name("direction")] public string Direction { get; set; }

        [Name("timestamp")] public string Timestamp => "last_edited_time";
    }
}