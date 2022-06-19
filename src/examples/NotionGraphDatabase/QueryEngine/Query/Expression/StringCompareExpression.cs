namespace NotionGraphDatabase.QueryEngine.Query.Expression;

public class StringCompareExpression : ExpressionFunction
{
    public string Value { get; }

    public StringCompareExpression(string leftAlias, string leftPropertyName, string valueToCompare)
        : base(leftAlias, leftPropertyName)
    {
        Value = valueToCompare;
    }

    public override bool Matches(IPropertyValueResolver resolver)
    {
        var value = resolver.GetValue(LeftAlias, LeftPropertyName);
        return value is string s && s.Equals(Value);
    }

    public override string ToString()
    {
        return $"String Value Comparison filter: {LeftAlias}.{LeftPropertyName}={Value}";
    }
}