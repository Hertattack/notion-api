using NotionGraphDatabase.QueryEngine.Query.Expression;

namespace NotionGraphDatabase.QueryEngine.Query.Filter;

public class FilterExpression
{
    private readonly IQuery _query;
    private readonly NodeReference _nodeReference;
    private readonly string _propertyName;
    private readonly ExpressionFunction _expression;

    public FilterExpression(
        IQuery query,
        NodeReference nodeReference,
        string propertyName,
        ExpressionFunction expression)
    {
        _query = query;
        _nodeReference = nodeReference;
        _propertyName = propertyName;
        _expression = expression;
    }
}