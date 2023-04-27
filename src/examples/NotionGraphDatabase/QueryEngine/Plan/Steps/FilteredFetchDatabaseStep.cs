using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.Storage;
using NotionGraphDatabase.Storage.Filtering;
using Util.Extensions;

namespace NotionGraphDatabase.QueryEngine.Plan.Steps;

internal class FilteredFetchDatabaseStep : ExecutionPlanStep
{
    private readonly Database _database;
    private readonly Filter _filterExpression;

    public FilteredFetchDatabaseStep(Database database, Filter filterExpression)
    {
        _database = database;
        _filterExpression = filterExpression;
    }

    public override void Execute(QueryExecutionContext executionContext, IStorageBackend storageBackend)
    {
        var database = storageBackend.GetDatabase(_database.Id).ThrowIfNull();
        database.GetFiltered(_filterExpression);
    }

    public override string ToString()
    {
        return
            $"Fetch database with filter '{_database.Id}' ({_database.Alias}). Using filter expressions: {_filterExpression}";
    }
}