namespace NotionGraphDatabase.QueryEngine.Query;

public struct NodeReference
{
    public string NodeName { get; }
    public string Alias { get; }

    public NodeReference(string nodeName, string alias)
    {
        NodeName = nodeName;
        Alias = alias;
    }
}