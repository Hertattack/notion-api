namespace NotionGraphDatabase.Query;

internal class NodeAllPropertiesSelected : NodePropertySelection
{
    public NodeAllPropertiesSelected(NodeReference referencedNode) : base(referencedNode)
    {
    }

    public override bool MatchesOrExtends(NodePropertySelection otherSelection)
    {
        return otherSelection.ReferencedNode.Alias == ReferencedNode.Alias
               && otherSelection.ReferencedNode.NodeName == ReferencedNode.NodeName;
    }
}