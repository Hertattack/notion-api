namespace NotionGraphDatabase.Metadata;

public class Model
{
    public IList<Database> Databases { get; } = new List<Database>();
    public IList<EdgeDefinition> Edges { get; } = new List<EdgeDefinition>();
}