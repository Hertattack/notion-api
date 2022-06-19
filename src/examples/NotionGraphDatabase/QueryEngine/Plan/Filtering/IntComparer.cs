using NotionGraphDatabase.Storage.Filtering;
using NotionGraphDatabase.Storage.Filtering.Integer;

namespace NotionGraphDatabase.QueryEngine.Plan.Filtering;

internal static class IntComparer
{
    public static bool Compare(PropertyValueResolver resolver, IntEqualsFilterExpression equalsFilterExpression)
    {
        var value = resolver.GetValue(equalsFilterExpression.NodeAlias, equalsFilterExpression.PropertyName);

        return value switch
        {
            null =>
                false,
            int intValue =>
                intValue == equalsFilterExpression.Value,
            _ => false
        };
    }
}