using Microsoft.Extensions.Logging;
using NotionApi;
using NotionApi.Rest.Request.Database;
using NotionApi.Rest.Request.Parameter;
using NotionApi.Rest.Response.Page;
using NotionGraphDatabase.Storage.DataModel;

namespace NotionGraphDatabase.Storage;

public class CachingNotionStorageBackend : IStorageBackend
{
    private readonly INotionClient _notionClient;
    private readonly ILogger<CachingNotionStorageBackend> _logger;
    private readonly DataStore _dataStore;

    public CachingNotionStorageBackend(INotionClient notionClient, ILogger<CachingNotionStorageBackend> logger)
    {
        _notionClient = notionClient;
        _logger = logger;
        _dataStore = new DataStore();
    }

    public Database? GetDatabase(string databaseId, bool fetchPages)
    {
        var databaseRequest = new DatabaseDefinitionRequest {DatabaseId = databaseId};
        var response = _notionClient.ExecuteRequest(databaseRequest).Result;

        if (!response.HasValue)
        {
            _logger.LogError("Database: '{databaseId}' was not found.", databaseId);
            throw new StorageException($"Database: '{databaseId}' was not found.");
        }

        var definition = _dataStore.GetDatabase(databaseId);
        definition.UpdateDefinition(response.Value);

        if (fetchPages)
        {
            var databaseContentsRequest = new SearchDatabaseRequest {DatabaseId = databaseId};

            if (definition.HasPages())
            {
                var ts = definition.GetLastKnowEditTimestamp();
                databaseContentsRequest.Parameters.Filter = new DatabaseLastEditedTimestampFilter
                {
                    LastEditedTimeFilter = new EqualsTimeFilter(ts)
                };
            }

            var databaseContentsResponse = _notionClient.ExecuteRequest(databaseContentsRequest).Result;
            if (!databaseContentsResponse.HasValue)
            {
                _logger.LogTrace("Database: '{databaseId}' has no entries.", databaseId);
                return null;
            }

            var databaseContents = databaseContentsResponse.Value;
            definition.FullUpdate(databaseContents.Results.Select(no => (PageObject) no));
        }

        return definition;
    }
}