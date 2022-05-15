namespace NotionGraphDatabase.Query;

public class NodeReference
{
    public string NodeName { get; }
    public string Alias { get; }

    public NodeReference(string nodeName, string alias)
    {
        NodeName = nodeName;
        Alias = alias;
    }
}