using NotionGraphDatabase.QueryEngine.Query.Expression;

namespace NotionGraphDatabase.QueryEngine.Query.Filter;

public class FilterExpression
{
    private readonly IQuery _query;

    public NodeReference NodeReference { get; }

    public string PropertyName { get; }

    public ExpressionFunction Expression { get; }

    public FilterExpression(
        IQuery query,
        NodeReference nodeReference,
        string propertyName,
        ExpressionFunction expression)
    {
        _query = query;
        NodeReference = nodeReference;
        PropertyName = propertyName;
        Expression = expression;
    }
}