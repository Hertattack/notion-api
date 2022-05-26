namespace NotionGraphDatabase.QueryEngine.Query;

internal class NodeAllPropertiesSelected : NodePropertySelection
{
    public NodeAllPropertiesSelected(NodeReference nodeReference)
    {
        ReferencedNode = nodeReference;
    }

    public override bool MatchesOrExtends(NodePropertySelection otherSelection)
    {
        return otherSelection.ReferencedNode.Alias == ReferencedNode.Alias
               && otherSelection.ReferencedNode.NodeName == ReferencedNode.NodeName;
    }
}