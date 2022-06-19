using Microsoft.Extensions.Logging;
using NotionApi;
using NotionGraphDatabase.Storage.DataModel;
using NotionGraphDatabase.Storage.Filtering;

namespace NotionGraphDatabase.Storage;

public class CachingNotionStorageBackend : IStorageBackend
{
    private readonly DataStore _dataStore;

    public CachingNotionStorageBackend(
        INotionClient notionClient,
        ILoggerFactory loggerFactory)
    {
        _dataStore = new DataStore(notionClient, loggerFactory);
    }

    public Database GetDatabase(string databaseId)
    {
        return _dataStore.CreateOrRetrieveDatabase(databaseId);
    }

    public bool Supports(Filter filter)
    {
        return filter is StringComparisonExpression;
    }
}