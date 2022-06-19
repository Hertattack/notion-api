namespace NotionGraphDatabase.Query.Parser.Ast;

internal class NodeClassReference : SelectExpression
{
    public Identifier NodeIdentifier { get; }
    public Identifier Alias { get; }
    public FilterExpressionList Filter { get; }

    public NodeClassReference(Identifier nodeIdentifier, Identifier alias)
    {
        NodeIdentifier = nodeIdentifier;
        Alias = alias;
        Filter = new FilterExpressionList();
    }

    public NodeClassReference(Identifier nodeIdentifier)
    {
        NodeIdentifier = nodeIdentifier;
        Alias = nodeIdentifier;
        Filter = new FilterExpressionList();
    }

    public NodeClassReference(Identifier nodeIdentifier, Identifier alias, FilterExpressionList filterExpression)
    {
        NodeIdentifier = nodeIdentifier;
        Alias = alias;
        Filter = filterExpression;
    }
}