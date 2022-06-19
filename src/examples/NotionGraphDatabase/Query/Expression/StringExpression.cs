namespace NotionGraphDatabase.Query.Expression;

public class StringExpression : Expression
{
    public string Value { get; }

    public StringExpression(string valueToCompare)
    {
        Value = valueToCompare;
    }

    public override string ToString()
    {
        return $"String Value: {Value}";
    }
}