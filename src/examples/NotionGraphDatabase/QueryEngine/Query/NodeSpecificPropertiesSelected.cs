namespace NotionGraphDatabase.QueryEngine.Query;

public class NodeSpecificPropertiesSelected : NodePropertySelection
{
    private List<string> _propertyNames = new();


    public NodeSpecificPropertiesSelected(NodeReference referencedNode) : base(referencedNode)
    {
    }

    public override bool MatchesOrExtends(NodePropertySelection otherSelection)
    {
        throw new NotImplementedException();
    }
}