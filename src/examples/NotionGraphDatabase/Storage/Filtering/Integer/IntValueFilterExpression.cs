namespace NotionGraphDatabase.Storage.Filtering.Integer;

public class IntValueFilterExpression : PropertyFilterExpression
{
    public int Value { get; }

    protected IntValueFilterExpression(string nodeAlias, string propertyName, int value) : base(nodeAlias, propertyName)
    {
        Value = value;
    }
}