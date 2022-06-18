using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.Storage;
using NotionGraphDatabase.Util;
using Util.Extensions;

namespace NotionGraphDatabase.QueryEngine.Plan.Steps;

internal class FilteredFetchDatabaseStep : ExecutionPlanStep
{
    private readonly Database _database;
    private readonly IEnumerable<object> _filters;

    public FilteredFetchDatabaseStep(Database database, IEnumerable<object> filters)
    {
        _database = database;
        _filters = filters;
    }

    public override void Execute(QueryExecutionContext executionContext, IStorageBackend storageBackend)
    {
        storageBackend.GetDatabase(_database.Id.RemoveDashes()).ThrowIfNull().GetAll();
    }

    public override string ToString()
    {
        return $"Fetch database with filter '{_database.Id}' ({_database.Alias})";
    }
}