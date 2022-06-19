namespace NotionGraphDatabase.Query.Expression;

public class IntegerExpression : Expression
{
    public int Value { get; }

    public IntegerExpression(int valueToCompare)
    {
        Value = valueToCompare;
    }

    public override string ToString()
    {
        return $"Integer Value: {Value}";
    }
}