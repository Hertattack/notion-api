namespace NotionGraphDatabase.QueryEngine.Model;

internal class NodeClassReference : SelectExpression
{
    public Identifier NodeIdentifier { get; }
    public Identifier Alias { get; }
    public FilterExpressionList Filter { get; }

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

    public NodeClassReference(Identifier nodeIdentifier, Identifier alias, FilterExpressionList filterExpression)
    {
        NodeIdentifier = nodeIdentifier;
        Alias = alias;
        Filter = filterExpression;
    }
}