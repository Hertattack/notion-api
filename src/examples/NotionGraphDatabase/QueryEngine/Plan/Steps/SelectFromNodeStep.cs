using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.QueryEngine.Query.Filter;
using NotionGraphDatabase.Storage;
using NotionGraphDatabase.Util;
using Util.Extensions;

namespace NotionGraphDatabase.QueryEngine.Plan.Steps;

internal class SelectFromNodeStep : ExecutionPlanStep
{
    protected readonly Database _database;
    protected readonly string _alias;
    private readonly IList<FilterExpression> _filters;
    private readonly bool _noFilters;
    protected readonly PropertyValueResolver _resolver;

    public SelectFromNodeStep(Database database, string alias, IEnumerable<FilterExpression> filters)
    {
        _database = database;
        _alias = alias;
        _filters = filters.ToList();
        _noFilters = _filters.Count <= 0;
        _resolver = new PropertyValueResolver();
    }

    public override void Execute(QueryExecutionContext executionContext, IStorageBackend storageBackend)
    {
        var previousResultContext = executionContext.GetCurrentResultContext();

        if (previousResultContext is not null)
            throw new Exception("Only one select-step supported.");

        var database = storageBackend.GetDatabase(_database.Id.RemoveDashes(), false).ThrowIfNull();
        var nextResultContext = executionContext.GetNextResultContext(database.Properties, _alias);
        _resolver.SetContext(nextResultContext);

        nextResultContext.AddRange(
            database.Pages
                .Select(p => new IntermediateResultRow(p))
                .Where(r => ApplyFilters(r, nextResultContext))
        );
    }

    protected bool ApplyFilters(IntermediateResultRow row, IntermediateResultContext context)
    {
        return _noFilters
               || _filters.All(f => f.Expression.Matches(_resolver.SetRow(row)));
    }

    public override string ToString()
    {
        return $"Select node '{_database.Id}' ({_alias})";
    }
}