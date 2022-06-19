namespace NotionGraphDatabase.QueryEngine.Query.Expression;

public class StringCompareExpression : ExpressionFunction
{
    private readonly string _valueToCompare;
    public string Value => _valueToCompare;

    public StringCompareExpression(string leftAlias, string leftPropertyName, string valueToCompare)
        : base(leftAlias, leftPropertyName)
    {
        _valueToCompare = valueToCompare;
    }

    public override bool Matches(IPropertyValueResolver resolver)
    {
        var value = resolver.GetValue(LeftAlias, LeftPropertyName);
        return value is string s && s.Equals(_valueToCompare);
    }

    public override string ToString()
    {
        return $"String Value Comparison filter: {LeftAlias}.{LeftPropertyName}={_valueToCompare}";
    }
}