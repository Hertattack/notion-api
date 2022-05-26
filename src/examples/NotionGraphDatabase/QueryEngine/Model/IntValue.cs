namespace NotionGraphDatabase.QueryEngine.Model;

internal class IntValue : Expression
{
    public int Value { get; }

    public IntValue(int value)
    {
        Value = value;
    }
}