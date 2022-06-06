namespace NotionGraphDatabase.QueryEngine.Query;

public class NodeSpecificPropertiesSelected : NodePropertySelection
{
    private HashSet<string> _propertyNames = new();
    public IEnumerable<string> PropertyNames => _propertyNames;

    public NodeSpecificPropertiesSelected(NodeReference referencedNode) : base(referencedNode)
    {
    }

    public override bool MatchesOrExtends(NodePropertySelection otherSelection)
    {
        return otherSelection switch
        {
            NodeAllPropertiesSelected => false,
            NodeSpecificPropertiesSelected otherSpecificPropertySelection =>
                _propertyNames.IsSupersetOf(otherSpecificPropertySelection._propertyNames),
            _ => false
        };
    }

    public void Add(string propertyName)
    {
        _propertyNames.Add(propertyName);
    }
}