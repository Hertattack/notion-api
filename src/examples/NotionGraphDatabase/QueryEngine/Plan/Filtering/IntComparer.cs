using NotionGraphDatabase.Storage.Filtering;

namespace NotionGraphDatabase.QueryEngine.Plan.Filtering;

internal static class IntComparer
{
    public static bool Compare(PropertyValueResolver resolver, IntComparisonExpression comparisonExpression)
    {
        var value = resolver.GetValue(comparisonExpression.NodeAlias, comparisonExpression.PropertyName);

        return value switch
        {
            null =>
                false,
            int intValue =>
                intValue == comparisonExpression.Value,
            _ => false
        };
    }
}