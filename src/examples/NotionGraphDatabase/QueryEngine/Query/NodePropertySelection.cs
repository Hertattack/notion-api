namespace NotionGraphDatabase.QueryEngine.Query;

public abstract class NodePropertySelection
{
    public NodeReference ReferencedNode { get; }

    protected NodePropertySelection(NodeReference referencedNode)
    {
        ReferencedNode = referencedNode;
    }

    public abstract bool MatchesOrExtends(NodePropertySelection otherSelection);
}