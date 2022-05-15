namespace NotionGraphDatabase.QueryEngine.Model;

internal class NodeClassReference : SelectExpression
{
    public Identifier NodeIdentifier { get; }
    public Identifier Alias { get; }

    public NodeClassReference(Identifier nodeIdentifier, Identifier alias)
    {
        NodeIdentifier = nodeIdentifier;
        Alias = alias;
    }

    public NodeClassReference(Identifier nodeIdentifier)
    {
        NodeIdentifier = nodeIdentifier;
        Alias = nodeIdentifier;
    }
}