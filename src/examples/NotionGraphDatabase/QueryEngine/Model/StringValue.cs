namespace NotionGraphDatabase.QueryEngine.Model;

internal class StringValue : Expression
{
    public string Value { get; }

    public StringValue(string value)
    {
        Value = value;
    }
}