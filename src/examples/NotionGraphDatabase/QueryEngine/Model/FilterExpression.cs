namespace NotionGraphDatabase.QueryEngine.Model;

internal class FilterExpression : QueryPredicate
{
    public Identifier PropertyIdentifier { get; }
    public Expression Expression { get; }

    public FilterExpression(Identifier propertyIdentifier, Expression expression)
    {
        PropertyIdentifier = propertyIdentifier;
        Expression = expression;
    }
}