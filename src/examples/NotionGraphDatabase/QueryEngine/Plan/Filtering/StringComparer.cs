using NotionGraphDatabase.Storage.Filtering;

namespace NotionGraphDatabase.QueryEngine.Plan.Filtering;

internal static class StringComparer
{
    public static bool Compare(PropertyValueResolver resolver, StringEqualsExpression equalsExpression)
    {
        var value = resolver.GetValue(equalsExpression.NodeAlias, equalsExpression.PropertyName);

        return value switch
        {
            null =>
                false,
            string stringValue =>
                stringValue == equalsExpression.Value,
            _ => false
        };
    }
}