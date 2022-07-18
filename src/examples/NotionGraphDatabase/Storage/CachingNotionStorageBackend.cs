using Microsoft.Extensions.Logging;
using NotionApi;
using NotionApi.Rest.Request.Database;
using NotionGraphDatabase.Storage.DataModel;
using NotionGraphDatabase.Storage.Filtering;
using NotionGraphDatabase.Storage.Filtering.String;
using NotionGraphDatabase.Util;

namespace NotionGraphDatabase.Storage;

public class CachingNotionStorageBackend : IStorageBackend
{
    private readonly INotionClient _notionClient;
    private readonly ILogger<CachingNotionStorageBackend> _logger;
    private readonly DataStore _dataStore;

    public CachingNotionStorageBackend(
        INotionClient notionClient,
        ILoggerFactory loggerFactory,
        ILogger<CachingNotionStorageBackend> logger)
    {
        _notionClient = notionClient;
        _logger = logger;
        _dataStore = new DataStore(notionClient, loggerFactory.CreateLogger<Database>());
    }

    public Database GetDatabase(string databaseId)
    {
        var definition = GetDatabaseDefinition(databaseId);
        return _dataStore.CreateOrRetrieveDatabase(definition);
    }

    public DatabaseDefinition GetDatabaseDefinition(string databaseId)
    {
        var databaseRequest = new DatabaseDefinitionRequest {DatabaseId = databaseId.RemoveDashes()};
        var response = _notionClient.ExecuteRequest(databaseRequest).Result;

        if (response.HasValue)
        {
            var notionRepresentation = response.Value;
            return new DatabaseDefinition(databaseId, notionRepresentation);
        }

        _logger.LogError("Database: '{DatabaseId}' was not found", databaseId);
        throw new StorageException($"Database: '{databaseId}' was not found.");
    }

    public bool Supports(Filter filter)
    {
        return filter is StringValueFilterExpression;
    }
}