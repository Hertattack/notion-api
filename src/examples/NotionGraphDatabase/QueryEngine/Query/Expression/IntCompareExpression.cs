namespace NotionGraphDatabase.QueryEngine.Query.Expression;

public class IntCompareExpression : ExpressionFunction
{
    private readonly int _valueToCompare;

    public IntCompareExpression(int valueToCompare)
    {
        _valueToCompare = valueToCompare;
    }

    public override bool Matches(object value)
    {
        return value is int && value.Equals(_valueToCompare);
    }
}