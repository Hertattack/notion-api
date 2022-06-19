namespace NotionGraphDatabase.Metadata;

public class EdgeDefinition
{
    public EdgeSource From { get; set; } = null!;
    public EdgeSource To { get; set; } = null!;

    public EdgeNavigability Navigability { get; set; } = new();
}