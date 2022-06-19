using NotionGraphDatabase.Query.Expression;
using NotionGraphDatabase.QueryEngine.Execution;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal class PropertyValueResolver : IPropertyValueResolver
{
    private IntermediateResultRow _row = null!;
    private string _currentAlias = null!;

    public void SetRow(IntermediateResultRow row)
    {
        _row = row;
    }

    public void SetContext(IntermediateResultContext context)
    {
        _currentAlias = context.Alias;
    }

    public object? GetValue(string alias, string propertyName)
    {
        if (alias == _currentAlias)
            return _row[propertyName];

        var parentRow = _row.GetParentByAlias(alias);
        return parentRow[propertyName];
    }
}