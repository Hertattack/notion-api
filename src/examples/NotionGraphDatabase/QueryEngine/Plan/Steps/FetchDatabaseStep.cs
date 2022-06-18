using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.Storage;
using NotionGraphDatabase.Util;
using Util.Extensions;

namespace NotionGraphDatabase.QueryEngine.Plan.Steps;

internal class FetchDatabaseStep : ExecutionPlanStep
{
    private readonly Database _database;

    public FetchDatabaseStep(Database database)
    {
        _database = database;
    }

    public override void Execute(QueryExecutionContext executionContext, IStorageBackend storageBackend)
    {
        storageBackend.GetDatabase(_database.Id.RemoveDashes()).ThrowIfNull().GetAll();
    }

    public override string ToString()
    {
        return $"Fetch full database '{_database.Id}' ({_database.Alias})";
    }
}