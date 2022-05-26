namespace NotionGraphDatabase.QueryEngine.Query;

public abstract class NodePropertySelection
{
    public NodeReference ReferencedNode { get; protected set; }

    public abstract bool MatchesOrExtends(NodePropertySelection otherSelection);
}