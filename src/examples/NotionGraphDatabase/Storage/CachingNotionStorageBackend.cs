using Microsoft.Extensions.Logging;
using NotionApi;
using NotionGraphDatabase.Storage.DataModel;
using NotionGraphDatabase.Storage.Filtering;

namespace NotionGraphDatabase.Storage;

public class CachingNotionStorageBackend : IStorageBackend
{
    private readonly INotionClient _notionClient;
    private readonly ILogger<CachingNotionStorageBackend> _logger;
    private readonly DataStore _dataStore;

    public CachingNotionStorageBackend(
        INotionClient notionClient,
        ILogger<CachingNotionStorageBackend> logger,
        ILoggerFactory loggerFactory)
    {
        _notionClient = notionClient;
        _logger = logger;

        _dataStore = new DataStore(_notionClient, loggerFactory);
    }

    public Database GetDatabase(string databaseId)
    {
        return _dataStore.CreateOrRetrieveDatabase(databaseId);
    }

    public bool CanPushDown(Filter expression)
    {
        return false;
    }
}