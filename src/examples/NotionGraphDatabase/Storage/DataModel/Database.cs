using Microsoft.Extensions.Logging;
using NotionApi;
using NotionApi.Extensions;
using NotionApi.Rest.Request.Database;
using NotionApi.Rest.Request.Parameter;
using NotionApi.Rest.Response.Database;
using NotionApi.Rest.Response.Database.Properties;
using NotionApi.Rest.Response.Page;
using NotionGraphDatabase.Storage.Filtering;
using Util.Extensions;

namespace NotionGraphDatabase.Storage.DataModel;

public class Database : IDataStoreObject
{
    private readonly INotionClient _notionClient;
    private readonly ILogger<Database> _logger;
    private bool _deleted;
    private bool _allCached;

    private DataStore _store;

    private DatabaseObject? _notionRepresentation;

    private Dictionary<string, DatabasePage> _pages = new();

    private List<PropertyDefinition> _properties = new();

    public IEnumerable<PropertyDefinition> Properties =>
        _properties.AsReadOnly();

    public string Title { get; internal set; } = string.Empty;
    public string Id { get; }

    public Database(string databaseId, INotionClient notionClient, ILogger<Database> logger)
    {
        _notionClient = notionClient;
        _logger = logger;
        Id = databaseId;
    }

    public IEnumerable<DatabasePage> Pages => _pages.Values.ToList();

    public void UpdateDefinition()
    {
        if (_deleted)
            throw new StorageException("Updating deleted definition.");

        var databaseRequest = new DatabaseDefinitionRequest {DatabaseId = Id};
        var response = _notionClient.ExecuteRequest(databaseRequest).Result;

        if (response.HasValue)
        {
            _notionRepresentation = response.Value;
        }
        else
        {
            _logger.LogError("Database: '{DatabaseId}' was not found", Id);
            throw new StorageException($"Database: '{Id}' was not found.");
        }

        Title = _notionRepresentation.Title.ToPlainTextString();
        _properties = _notionRepresentation.Properties
            .Select(kvp => CreatePropertyDefinition(kvp.Key, kvp.Value))
            .ToList();
    }

    private static PropertyDefinition CreatePropertyDefinition(string propertyName,
        NotionPropertyConfiguration configuration)
    {
        return configuration switch
        {
            FormulaPropertyConfiguration => new PropertyDefinition(propertyName, configuration.Type),
            NumberPropertyConfiguration => new PropertyDefinition(propertyName, configuration.Type),
            RelationPropertyConfiguration => new PropertyDefinition(propertyName, configuration.Type),
            RollupPropertyConfiguration => new PropertyDefinition(propertyName, configuration.Type),
            SelectPropertyConfiguration => new PropertyDefinition(propertyName, configuration.Type),
            MultiSelectPropertyConfiguration => new PropertyDefinition(propertyName, configuration.Type),
            _ => new PropertyDefinition(propertyName, configuration.Type)
        };
    }

    public IEnumerable<DatabasePage> GetAll()
    {
        RetrievePages();
        return Pages;
    }

    public IEnumerable<DatabasePage> GetFiltered(Filter filter)
    {
        var databaseContentsRequest = new SearchDatabaseRequest
        {
            DatabaseId = Id,
            Parameters =
            {
                Filter = MapToNotionFilter(filter)
            }
        };

        var databaseContentsResponse = _notionClient.ExecuteRequest(databaseContentsRequest).Result;
        if (!databaseContentsResponse.HasValue)
        {
            _logger.LogTrace("Database: '{DatabaseId}' has no entries", Id);
            return Array.Empty<DatabasePage>();
        }

        var databaseContents = databaseContentsResponse.Value;
        var resultsFromNotionApi = databaseContents.Results.Select(no => (PageObject) no);
        return UpdateAndInsert(resultsFromNotionApi);
    }

    private DatabaseFilter MapToNotionFilter(Filter filter)
    {
        return null;
    }

    public void Delete()
    {
        _deleted = true;
        _notionRepresentation = null;
    }

    public DateTime? GetLastKnowEditTimestamp()
    {
        if (_pages.Count == 0)
            return null;

        return _pages.Max(p => p.Value.LastEditTimestamp);
    }

    public bool HasPages()
    {
        return _pages.Count > 0;
    }

    internal void RetrievePages()
    {
        var databaseContentsRequest = new SearchDatabaseRequest {DatabaseId = Id};
        var fullUpdate = true;

        if (_allCached && HasPages())
        {
            var ts = GetLastKnowEditTimestamp();

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
            _logger.LogTrace("Database: '{DatabaseId}' has no entries", Id);
            return;
        }

        var databaseContents = databaseContentsResponse.Value;
        var resultsFromNotionApi = databaseContents.Results.Select(no => (PageObject) no);
        if (fullUpdate)
        {
            _logger.LogDebug("Performing full update from Notion for database: '{DatabaseTitle}' ({DatabaseId})",
                Title, Id);
            FullUpdate(resultsFromNotionApi);
        }
        else
        {
            UpdateAndInsert(resultsFromNotionApi);
        }
    }

    private void FullUpdate(IEnumerable<PageObject> allPages)
    {
        if (_deleted)
            return;

        var all = allPages.ToList();
        var index = all.ToDictionary(p => p.Id);

        foreach (var toDelete in _pages.Keys.Except(index.Keys))
        {
            _pages.Remove(toDelete, out var deletedPage);
            deletedPage.ThrowIfNull().Delete();
        }

        UpdateAndInsert(all);

        _allCached = true;
    }

    private IEnumerable<DatabasePage> UpdateAndInsert(IEnumerable<PageObject> updatedAndNewPages)
    {
        var input = updatedAndNewPages.ToList();
        if (_pages.Count == 0)
        {
            _pages = input.ToDictionary(p => p.Id,
                p => new DatabasePage(this, p));
            return _pages.Values;
        }

        var result = new List<DatabasePage>();
        foreach (var updatedPage in input)
        {
            _pages.TryGetValue(updatedPage.Id, out var existingPage);

            if (existingPage is null)
            {
                existingPage = new DatabasePage(this, updatedPage);
                _pages[updatedPage.Id] = existingPage;
            }
            else
            {
                existingPage.Update(updatedPage);
            }

            result.Add(existingPage);
        }

        return result;
    }

    public PropertyDefinition GetProperty(string propertyName)
    {
        return _properties.First(p => p.Name == propertyName);
    }
}