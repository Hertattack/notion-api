namespace NotionGraphDatabase.QueryEngine.Query.Expression;

public class StringCompareExpression : ExpressionFunction
{
    private readonly string _valueToCompare;

    public StringCompareExpression(string valueToCompare)
    {
        _valueToCompare = valueToCompare;
    }
}