namespace NotionGraphDatabase.Query.Expression;

public class IntCompareExpression : ExpressionFunction
{
    public int Value { get; }

    public IntCompareExpression(string leftAlias, string leftPropertyName, int valueToCompare)
        : base(leftAlias, leftPropertyName)
    {
        Value = valueToCompare;
    }

    public override string ToString()
    {
        return $"Integer Value Comparison filter: {LeftAlias}.{LeftPropertyName}={Value}";
    }
}