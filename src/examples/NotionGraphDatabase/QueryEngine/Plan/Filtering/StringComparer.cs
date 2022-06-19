using NotionGraphDatabase.Storage.Filtering;

namespace NotionGraphDatabase.QueryEngine.Plan.Filtering;

internal static class StringComparer
{
    public static bool Compare(PropertyValueResolver resolver, StringComparisonExpression comparisonExpression)
    {
        var value = resolver.GetValue(comparisonExpression.NodeAlias, comparisonExpression.PropertyName);

        return value switch
        {
            null =>
                false,
            string stringValue =>
                stringValue == comparisonExpression.Value,
            _ => false
        };
    }
}