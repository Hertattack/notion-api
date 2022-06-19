using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.QueryEngine.Execution.Filtering;
using NotionGraphDatabase.Storage.Filtering;
using Util.Extensions;

namespace NotionGraphDatabase.QueryEngine.Plan.Filtering;

internal class FilterEngine
{
    private readonly PropertyValueResolver _resolver;
    private readonly Filter? _filter;
    private readonly bool _hasFilter;

    public FilterEngine(PropertyValueResolver resolver, Filter? filter)
    {
        _resolver = resolver;
        _filter = filter;
        _hasFilter = _filter is not null;
    }

    public bool Matches(IntermediateResultRow row)
    {
        if (!_hasFilter)
            return true;

        _resolver.SetRow(row);

        return Matches(row, _filter.ThrowIfNull());
    }

    private bool Matches(IntermediateResultRow row, Filter currentFilter)
    {
        return currentFilter switch
        {
            EmptyFilterExpression =>
                true,
            IntComparisonExpression intValueComparisonExpression =>
                IntComparer.Compare(_resolver, intValueComparisonExpression),
            StringComparisonExpression stringComparisonExpression =>
                StringComparer.Compare(_resolver, stringComparisonExpression),
            PropertyComparisonExpression propertyComparisonExpression =>
                PropertyComparer.Compare(_resolver, propertyComparisonExpression),
            _ => throw new Exception($"Unsupported filter expression: '{currentFilter.GetType().FullName}'")
        };
    }


    private bool CompareValue(IntermediateResultRow row, IntComparisonExpression comparisonExpression)
    {
        var value = _resolver.GetValue(comparisonExpression.NodeAlias,
            comparisonExpression.PropertyName);

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