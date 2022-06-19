namespace NotionGraphDatabase.Query.Filter;

public class FilterExpression
{
    public string Alias { get; }
    public string PropertyName { get; }
    public ComparisonOperator Operator { get; }
    public Expression.Expression Expression { get; }

    public FilterExpression(
        string alias, string propertyName,
        ComparisonOperator @operator,
        Expression.Expression expression)
    {
        Alias = alias;
        PropertyName = propertyName;
        Operator = @operator;
        Expression = expression;
    }

    public override string ToString()
    {
        return $"Filter expression on property: {Alias}.{PropertyName} {Operator} {Expression}";
    }
}