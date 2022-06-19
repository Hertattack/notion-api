using NotionGraphDatabase.QueryEngine.Execution.Filtering;

namespace NotionGraphDatabase.QueryEngine.Plan.Filtering;

internal static class PropertyComparer
{
    public static bool Compare(PropertyValueResolver resolver, PropertyComparisonExpression comparisonExpression)
    {
        var leftValue = resolver.GetValue(comparisonExpression.NodeAlias, comparisonExpression.PropertyName);
        var rightValue = resolver.GetValue(comparisonExpression.RightNodeAlias, comparisonExpression.RightPropertyName);
        return leftValue == rightValue;
    }
}