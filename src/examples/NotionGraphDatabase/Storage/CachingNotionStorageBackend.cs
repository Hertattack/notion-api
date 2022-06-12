using Microsoft.Extensions.Logging;
using NotionApi;
using NotionApi.Extensions;
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

    public CachingNotionStorageBackend(
        INotionClient notionClient,
        ILogger<CachingNotionStorageBackend> logger)
    {
        _notionClient = notionClient;
        _logger = logger;

        _dataStore = new DataStore();
    }

    public Database GetDatabase(string databaseId, bool retrieveAllPages)
    {
        var databaseRequest = new DatabaseDefinitionRequest {DatabaseId = databaseId};
        var response = _notionClient.ExecuteRequest(databaseRequest).Result;

        if (!response.HasValue)
        {
            _logger.LogError("Database: '{DatabaseId}' was not found", databaseId);
            throw new StorageException($"Database: '{databaseId}' was not found.");
        }

        var databaseTitle = response.Value.Title.ToPlainTextString();
        var database = _dataStore.CreateOrRetrieveDatabase(databaseId, databaseTitle);
        database.UpdateDefinition(response.Value);

        if (!retrieveAllPages)
            return database;

        RetrievePages(database);

        return database;
    }

    private void RetrievePages(Database database)
    {
        var databaseContentsRequest = new SearchDatabaseRequest {DatabaseId = database.Id};
        var fullUpdate = true;

        if (database.HasPages())
        {
            var ts = database.GetLastKnowEditTimestamp();

            if (ts is not null)
            {
                fullUpdate = false;
                databaseContentsRequest.Parameters.Filter = new DatabaseLastEditedTimestampFilter
                {
                    LastEditedTimeFilter = new OnOrAfterDateTimeFilter(ts.Value)
                };
            }
        }

        var databaseContentsResponse = _notionClient.ExecuteRequest(databaseContentsRequest).Result;
        if (!databaseContentsResponse.HasValue)
        {
            _logger.LogTrace("Database: '{DatabaseId}' has no entries", database.Id);
            return;
        }

        var databaseContents = databaseContentsResponse.Value;
        var resultsFromNotionApi = databaseContents.Results.Select(no => (PageObject) no);
        if (fullUpdate)
        {
            _logger.LogDebug("Performing full update from Notion for database: '{DatabaseTitle}' ({DatabaseId})",
                database.Title, database.Id);
            database.FullUpdate(resultsFromNotionApi);
        }
        else
        {
            database.UpdateAndInsert(resultsFromNotionApi);
        }
    }
}