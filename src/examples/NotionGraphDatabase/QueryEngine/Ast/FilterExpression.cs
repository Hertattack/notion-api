namespace NotionGraphDatabase.QueryEngine.Ast;

internal class FilterExpression : QueryPredicate
{
    public Identifier? NodeIdentifier { get; }
    public PropertyName PropertyName { get; }
    public Expression Expression { get; }

    public FilterExpression(Identifier nodeIdentifier, PropertyName propertyName, Expression expression)
    {
        NodeIdentifier = nodeIdentifier;
        PropertyName = propertyName;
        Expression = expression;
    }

    public FilterExpression(PropertyName propertyName, Expression expression)
    {
        NodeIdentifier = null;
        PropertyName = propertyName;
        Expression = expression;
    }
}