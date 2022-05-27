namespace NotionGraphDatabase.QueryEngine.Query.Expression;

internal class PropertyValueCompareExpression : ExpressionFunction
{
    public string NodeAlias { get; }

    public string PropertyName { get; }

    public PropertyValueCompareExpression(string nodeAlias, string propertyName)
    {
        NodeAlias = nodeAlias;
        PropertyName = propertyName;
    }

    public override bool Matches(object value)
    {
        return false;
    }
}