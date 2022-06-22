using NotionGraphDatabase.Storage.Filtering;

namespace NotionGraphDatabase.QueryEngine.Plan.Filtering;

public class UnsupportedFilterExpressionException : Exception
{
    public UnsupportedFilterExpressionException(Filter expression)
        : base($"Unsupported filter type: {expression.GetType().FullName}")
    {
    }
}