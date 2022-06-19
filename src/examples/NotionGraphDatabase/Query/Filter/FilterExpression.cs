using NotionGraphDatabase.Query.Expression;

namespace NotionGraphDatabase.Query.Filter;

public class FilterExpression
{
    private readonly IQuery _query;

    public string Alias { get; }

    public string PropertyName { get; }

    public ExpressionFunction Expression { get; }

    public FilterExpression(
        IQuery query,
        string alias,
        string propertyName,
        ExpressionFunction expression)
    {
        _query = query;
        Alias = alias;
        PropertyName = propertyName;
        Expression = expression;
    }
}