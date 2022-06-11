using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.Storage;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal class FetchDatabaseStep : ExecutionPlanStep
{
    private readonly Database _database;

    public FetchDatabaseStep(Database database)
    {
        _database = database;
    }

    public override void Execute(QueryExecutionContext executionContext, IStorageBackend storageBackend)
    {
        storageBackend.GetDatabase(_database.Id.Replace("-", ""));
    }
}