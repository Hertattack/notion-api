namespace NotionGraphDatabase.QueryEngine.Query.Expression;

public class StringCompareExpression : ExpressionFunction
{
    private readonly string _valueToCompare;

    public StringCompareExpression(string valueToCompare)
    {
        _valueToCompare = valueToCompare;
    }

    public override bool Matches(object value)
    {
        return value is string s && s.Equals(_valueToCompare);
    }
}