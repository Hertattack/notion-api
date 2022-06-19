using NotionGraphDatabase.Storage.Filtering;

namespace NotionGraphDatabase.QueryEngine.Execution.Filtering;

internal class PropertyComparisonExpression : PropertyFilterExpression
{
    public string RightNodeAlias { get; }
    public string RightPropertyName { get; }

    public PropertyComparisonExpression(
        string leftNodeAlias, string leftPropertyName,
        string rightNodeAlias, string rightPropertyName)
        : base(leftNodeAlias, leftPropertyName)
    {
        RightNodeAlias = rightNodeAlias;
        RightPropertyName = rightPropertyName;
    }
}