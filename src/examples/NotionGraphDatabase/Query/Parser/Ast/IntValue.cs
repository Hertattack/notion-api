namespace NotionGraphDatabase.Query.Parser.Ast;

internal class IntValue : Expression
{
    public int Value { get; }

    public IntValue(int value)
    {
        Value = value;
    }
}