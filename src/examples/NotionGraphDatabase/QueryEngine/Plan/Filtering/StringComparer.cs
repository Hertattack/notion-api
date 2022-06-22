using NotionGraphDatabase.Storage.Filtering.String;

namespace NotionGraphDatabase.QueryEngine.Plan.Filtering;

internal static class StringComparer
{
    public static bool Compare(PropertyValueResolver resolver, StringValueFilterExpression filterExpression)
    {
        var value = resolver.GetValue(filterExpression.NodeAlias, filterExpression.PropertyName);

        return filterExpression switch
        {
            StringEqualsExpression => value switch
            {
                string stringValue => stringValue == filterExpression.Value,
                _ => false
            },
            StringNotEqualsFilterExpression => value switch
            {
                string stringValue => stringValue != filterExpression.Value,
                _ => false
            },
            StringContainsFilterExpression => value switch
            {
                string stringValue => stringValue.Contains(filterExpression.Value),
                _ => false
            },
            StringDoesNotContainFilterExpression => value switch
            {
                string stringValue => !stringValue.Contains(filterExpression.Value),
                _ => false
            },
            _ => throw new UnsupportedFilterExpressionException(filterExpression)
        };
    }
}