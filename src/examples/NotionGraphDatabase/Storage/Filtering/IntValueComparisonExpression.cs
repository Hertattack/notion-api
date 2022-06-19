namespace NotionGraphDatabase.Storage.Filtering;

public class IntValueComparisonExpression : PropertyFilterExpression
{
    public int Value { get; }

    public IntValueComparisonExpression(string nodeAlias, string propertyName, int value) : base(nodeAlias, propertyName)
    {
        Value = value;
    }
}