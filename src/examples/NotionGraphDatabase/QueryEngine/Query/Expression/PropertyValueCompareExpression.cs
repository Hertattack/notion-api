namespace NotionGraphDatabase.QueryEngine.Query.Expression;

internal class PropertyValueCompareExpression : ExpressionFunction
{
    private readonly string _nodeAlias;
    private readonly string _propertyName;

    public PropertyValueCompareExpression(string nodeAlias, string propertyName)
    {
        _nodeAlias = nodeAlias;
        _propertyName = propertyName;
    }
}