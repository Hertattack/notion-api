using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.QueryEngine.Query.Expression;
using NotionGraphDatabase.QueryEngine.Query.Filter;
using NotionGraphDatabase.Storage;
using Util.Extensions;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal class SelectFromNodeStep : ExecutionPlanStep
{
    private readonly Database _database;
    private readonly string _alias;
    private readonly IList<FilterExpression> _filters;
    private readonly bool _noFilters;
    private readonly PropertyValueResolver _resolver;

    public SelectFromNodeStep(Database database, string alias, IEnumerable<FilterExpression> filters)
    {
        _database = database;
        _alias = alias;
        _filters = filters.ToList();
        _noFilters = _filters.Count > 0;
        _resolver = new PropertyValueResolver();
    }

    public override void Execute(QueryExecutionContext context, IStorageBackend storageBackend)
    {
        var previousResultContext = context.GetCurrentResultContext();

        if (previousResultContext is not null)
            throw new Exception("Only one select-step supported.");

        var nextResultContext = context.GetNextResultContext(_alias);
        _resolver.SetContext(nextResultContext);

        var database = storageBackend.GetDatabase(_database.Id);
        nextResultContext.AddRange(
            database.ThrowIfNull().Pages
                .Select(p => new IntermediateResultRow(p))
                .Where(r => ApplyFilters(r, nextResultContext))
        );
    }

    private bool ApplyFilters(IntermediateResultRow row, IntermediateResultContext context)
    {
        return !_noFilters
               || _filters.All(f => f.Expression.Matches(_resolver.SetRow(row)));
    }
}

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