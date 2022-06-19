namespace NotionGraphDatabase.QueryEngine.Query.Expression;

public class IntCompareExpression : ExpressionFunction
{
    public int Value { get; }

    public IntCompareExpression(string leftAlias, string leftPropertyName, int valueToCompare)
        : base(leftAlias, leftPropertyName)
    {
        Value = valueToCompare;
    }

    public override bool Matches(IPropertyValueResolver resolver)
    {
        var value = resolver.GetValue(LeftAlias, LeftPropertyName);
        return value is int && value.Equals(Value);
    }

    public override string ToString()
    {
        return $"Integer Value Comparison filter: {LeftAlias}.{LeftPropertyName}={Value}";
    }
}