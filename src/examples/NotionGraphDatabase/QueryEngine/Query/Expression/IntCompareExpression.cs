namespace NotionGraphDatabase.QueryEngine.Query.Expression;

public class IntCompareExpression : ExpressionFunction
{
    private readonly int _valueToCompare;

    public IntCompareExpression(string leftAlias, string leftPropertyName, int valueToCompare)
        : base(leftAlias, leftPropertyName)
    {
        _valueToCompare = valueToCompare;
    }

    public override bool Matches(object value)
    {
        return value is int && value.Equals(_valueToCompare);
    }

    public override string ToString()
    {
        return $"Integer Value Comparison filter: {LeftAlias}.{LeftPropertyName}={_valueToCompare}";
    }
}