namespace NotionGraphDatabase.Query.Parser.Ast;

internal class FilterExpression : QueryPredicate
{
    public Identifier? NodeIdentifier { get; }
    public PropertyName PropertyName { get; }
    public Operator Operator { get; }
    public Expression Expression { get; }

    public FilterExpression(
        Identifier nodeIdentifier, PropertyName propertyName,
        Operator @operator,
        Expression expression)
    {
        NodeIdentifier = nodeIdentifier;
        PropertyName = propertyName;
        Operator = @operator;
        Expression = expression;
    }

    public FilterExpression(PropertyName propertyName, Operator @operator, Expression expression)
    {
        NodeIdentifier = null;
        PropertyName = propertyName;
        Operator = @operator;
        Expression = expression;
    }
}