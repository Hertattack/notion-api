namespace NotionGraphDatabase.Metadata;

public class EdgeNavigability
{
    public NavigationDetails Forward { get; set; } = null!;
    public NavigationDetails Reverse { get; set; } = null!;
}