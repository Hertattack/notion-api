using NotionGraphDatabase.Query.Expression;
using NotionGraphDatabase.QueryEngine.Execution;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal class PropertyValueResolver : IPropertyValueResolver
{
    private IntermediateResultRow _row;
    private IEnumerable<string> _aliases;
    private string _currentAlias;

    public IPropertyValueResolver SetRow(IntermediateResultRow row)
    {
        _row = row;
        return this;
    }

    public void SetContext(IntermediateResultContext context)
    {
        _currentAlias = context.Alias;
        _aliases = context.SelectedAliases();
    }

    public object? GetValue(string alias, string propertyName)
    {
        if (alias == _currentAlias)
            return _row[propertyName];

        var parentRow = _row.GetParentByAlias(alias);
        return parentRow[propertyName];
    }
}