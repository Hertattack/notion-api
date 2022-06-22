using NotionGraphDatabase.Storage.Filtering.Integer;

namespace NotionGraphDatabase.QueryEngine.Plan.Filtering;

internal static class IntComparer
{
    public static bool Compare(PropertyValueResolver resolver, IntValueFilterExpression filterExpression)
    {
        var value = resolver.GetValue(filterExpression.NodeAlias, filterExpression.PropertyName);

        return filterExpression switch
        {
            IntEqualsFilterExpression => value switch
            {
                int intValue => intValue == filterExpression.Value,
                _ => false
            },
            IntNotEqualsFilterExpression => value switch
            {
                int intValue => intValue != filterExpression.Value,
                _ => false
            },
            _ => throw new UnsupportedFilterExpressionException(filterExpression)
        };
    }
}