using NotionGraphDatabase.Interface;
using NotionGraphDatabase.Interface.Result;
using NotionGraphDatabase.QueryEngine;
using NotionGraphDatabase.Storage;
using NotionGraphDatabase.Storage.DataModel;

namespace NotionGraphDatabase;

public class GraphDatabase : IGraphDatabase
{
    private readonly IQueryEngine _queryEngine;
    private readonly IStorageBackend _storageBackend;

    public GraphDatabase(
        IQueryEngine queryEngine,
        IStorageBackend storageBackend)
    {
        _queryEngine = queryEngine;
        _storageBackend = storageBackend;
    }

    public QueryResult Execute(string queryText)
    {
        return _queryEngine.Execute(queryText);
    }

    public DatabaseDefinition GetDatabaseDefinition(string databaseId)
    {
        return _storageBackend.GetDatabaseDefinition(databaseId);
    }
}