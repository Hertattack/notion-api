using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.QueryEngine.Execution.Filtering;
using NotionGraphDatabase.Storage.Filtering;
using NotionGraphDatabase.Storage.Filtering.Integer;
using NotionGraphDatabase.Storage.Filtering.String;
using Util.Extensions;

namespace NotionGraphDatabase.QueryEngine.Plan.Filtering;

internal class FilterEngine
{
    private readonly PropertyValueResolver _resolver;
    private readonly Filter? _filter;
    public bool HasFilter { get; }

    public FilterEngine(PropertyValueResolver resolver, Filter? filter)
    {
        _resolver = resolver;
        _filter = filter;
        HasFilter = _filter is not null && _filter is not EmptyFilterExpression;
    }

    public string GetFilterDescription()
    {
        if (_filter is EmptyFilterExpression)
            return "none";

        return _filter?.ToString() ?? "none";
    }


    public bool Matches(IntermediateResultRow row)
    {
        if (!HasFilter)
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
            IntValueFilterExpression intValueFilterExpression =>
                IntComparer.Compare(_resolver, intValueFilterExpression),
            StringValueFilterExpression stringValueFilterExpression =>
                StringComparer.Compare(_resolver, stringValueFilterExpression),
            PropertyComparisonExpression propertyComparisonExpression =>
                PropertyComparer.Compare(_resolver, propertyComparisonExpression),
            _ => throw new Exception($"Unsupported filter expression: '{currentFilter.GetType().FullName}'")
        };
    }


    private bool CompareValue(IntermediateResultRow row, IntEqualsFilterExpression equalsFilterExpression)
    {
        var value = _resolver.GetValue(equalsFilterExpression.NodeAlias,
            equalsFilterExpression.PropertyName);

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