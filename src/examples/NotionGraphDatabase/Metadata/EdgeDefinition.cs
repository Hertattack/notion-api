namespace NotionGraphDatabase.Metadata;

public class EdgeDefinition
{
    public EdgeSource From { get; set; }
    public EdgeSource To { get; set; }

    public EdgeNavigability Navigability { get; set; } = new();
}