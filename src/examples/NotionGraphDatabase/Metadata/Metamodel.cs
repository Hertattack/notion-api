namespace NotionGraphDatabase.Metadata;

public class Metamodel
{
    public IList<Database> Databases { get; } = new List<Database>();
    public IList<EdgeDefinition> Edges { get; } = new List<EdgeDefinition>();
}