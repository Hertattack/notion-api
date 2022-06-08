using NotionGraphDatabase.Interface.Result;
using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.QueryEngine.Query.Filter;
using NotionGraphDatabase.Storage;
using Util.Extensions;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal class SelectFromNodeStep : ExecutionPlanStep
{
    private readonly Database _database;
    private readonly string _alias;
    private readonly IList<FilterExpression> _filter;

    public SelectFromNodeStep(Database database, string alias, IEnumerable<FilterExpression> filter)
    {
        _database = database;
        _alias = alias;
        _filter = filter.ToList();
    }

    public override void Execute(QueryExecutionContext context, IStorageBackend storageBackend)
    {
        if (_filter.Count > 0)
            throw new Exception("Filtering not supported yet.");

        var previousResultContext = context.GetCurrentResultContext();

        if (previousResultContext is not null)
            throw new Exception("Only one select-step supported.");

        var nextResultContext = context.GetNextResultContext(_alias);

        var database = storageBackend.GetDatabase(_database.Id);
        nextResultContext.AddRange(database.ThrowIfNull().Pages.Select(p => new IntermediateResultRow(p)));
    }
}