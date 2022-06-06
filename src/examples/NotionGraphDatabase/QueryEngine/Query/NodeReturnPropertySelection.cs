namespace NotionGraphDatabase.QueryEngine.Query;

public class NodeReturnPropertySelection
{
    public NodeReference NodeReference { get; }
    public NodePropertySelection PropertySelection { get; }

    public NodeReturnPropertySelection(NodeReference nodeReference,
        NodePropertySelection propertySelection)
    {
        NodeReference = nodeReference;
        PropertySelection = propertySelection;
    }
}