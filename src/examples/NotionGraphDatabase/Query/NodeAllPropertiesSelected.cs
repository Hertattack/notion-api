namespace NotionGraphDatabase.Query;

internal class NodeAllPropertiesSelected : NodePropertySelection
{
    public NodeAllPropertiesSelected(NodeReference nodeReference)
    {
        ReferencedNode = nodeReference;
    }

    public override bool MatchesOrExtends(NodePropertySelection otherSelection)
    {
        return otherSelection.ReferencedNode == ReferencedNode;
    }
}