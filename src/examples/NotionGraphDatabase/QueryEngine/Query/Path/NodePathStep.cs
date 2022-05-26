namespace NotionGraphDatabase.QueryEngine.Query.Path;

public class NodePathStep : IPathStep
{
    private readonly NodeReference _nodeReference;

    public NodePathStep(NodeReference nodeReference)
    {
        _nodeReference = nodeReference;
    }

    public NodeReference AssociatedNode => _nodeReference;
}