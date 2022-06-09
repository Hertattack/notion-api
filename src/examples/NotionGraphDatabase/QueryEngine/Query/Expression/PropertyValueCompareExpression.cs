namespace NotionGraphDatabase.QueryEngine.Query.Expression;

internal class PropertyValueCompareExpression : ExpressionFunction
{
    public string RightAlias { get; }

    public string RightPropertyName { get; }

    public PropertyValueCompareExpression(
        string leftAlias, string leftPropertyName,
        string rightAlias, string rightPropertyName)
        : base(leftAlias, leftPropertyName)
    {
        RightAlias = rightAlias;
        RightPropertyName = rightPropertyName;
    }

    public override bool Matches(IPropertyValueResolver resolver)
    {
        return resolver.GetValue(LeftAlias, LeftPropertyName) == resolver.GetValue(RightAlias, RightPropertyName);
    }

    public override string ToString()
    {
        return $"Property Value Comparison filter: {LeftAlias}.{LeftPropertyName}={RightAlias}.{RightPropertyName}";
    }
}