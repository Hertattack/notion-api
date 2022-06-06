namespace NotionGraphDatabase.QueryEngine.Ast;

internal class StringValue : Expression
{
    public string Value { get; }

    public StringValue(string value)
    {
        Value = value;
    }
}