namespace NotionGraphDatabase.Storage.Filtering;

public class IntComparisonExpression : PropertyFilterExpression
{
    public int Value { get; }

    public IntComparisonExpression(string nodeAlias, string propertyName, int value) : base(nodeAlias, propertyName)
    {
        Value = value;
    }
}