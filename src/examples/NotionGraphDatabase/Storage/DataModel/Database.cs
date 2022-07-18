using Microsoft.Extensions.Logging;
using NotionApi;
using NotionApi.Rest.Request.Database;
using NotionApi.Rest.Request.Parameter;
using NotionApi.Rest.Response.Page;
using NotionGraphDatabase.Storage.Filtering;
using NotionGraphDatabase.Storage.Filtering.String;
using NotionGraphDatabase.Util;
using Util.Extensions;

namespace NotionGraphDatabase.Storage.DataModel;

public class Database : IDataStoreObject
{
    private readonly INotionClient _notionClient;
    private readonly ILogger<Database> _logger;
    private bool _deleted;
    private bool _allCached;

    private Dictionary<string, DatabasePage> _pages = new();

    public DatabaseDefinition Definition { get; private set; }

    public Database(DatabaseDefinition databaseDefinition, INotionClient notionClient, ILogger<Database> logger)
    {
        _notionClient = notionClient;
        _logger = logger;
        Definition = databaseDefinition;
    }

    public IEnumerable<DatabasePage> Pages => _pages.Values.ToList();


    public IEnumerable<DatabasePage> GetAll()
    {
        RetrievePages();
        return Pages;
    }

    public IEnumerable<DatabasePage> GetFiltered(Filter filter)
    {
        var databaseContentsRequest = new SearchDatabaseRequest
        {
            DatabaseId = Definition.Id.RemoveDashes(),
            Parameters =
            {
                Filter = MapToNotionFilter(filter)
            }
        };

        var databaseContentsResponse = _notionClient.ExecuteRequest(databaseContentsRequest).Result;
        if (!databaseContentsResponse.HasValue)
        {
            _logger.LogTrace("Database: '{DatabaseId}' has no entries", Definition.Id);
            return Array.Empty<DatabasePage>();
        }

        var databaseContents = databaseContentsResponse.Value;
        var resultsFromNotionApi = databaseContents.Results.Select(no => (PageObject) no);
        return UpdateAndInsert(resultsFromNotionApi);
    }

    private DatabaseFilter MapToNotionFilter(Filter filter)
    {
        if (filter is not PropertyFilterExpression propertyFilterExpression)
            throw new NotImplementedException(
                $"Unsupported filter type: {filter.GetType().FullName} - cannot map to Notion filter");

        var property = Definition.Properties.FirstOrDefault(p => p.Name == propertyFilterExpression.PropertyName);
        if (property is null)
            throw new UndefinedPropertyException(
                $"Cannot create filter for property: '{propertyFilterExpression.PropertyName}' - not found in database.");

        _logger.LogDebug("Applying Rich Text Filter to property with type: {PropertyType}", property.Type);

        return filter switch
        {
            StringEqualsExpression valueExpression => new RichTextPropertyFilter
            {
                PropertyName = property.Name, Filter = new TextFilter {EqualTo = valueExpression.Value}
            },
            StringNotEqualsFilterExpression valueExpression => new RichTextPropertyFilter
            {
                PropertyName = property.Name,
                Filter = new TextFilter {DoesNotEqual = valueExpression.Value}
            },
            StringContainsFilterExpression valueExpression => new RichTextPropertyFilter
            {
                PropertyName = property.Name,
                Filter = new TextFilter {Contains = valueExpression.Value}
            },
            StringDoesNotContainFilterExpression valueExpression => new RichTextPropertyFilter
            {
                PropertyName = property.Name,
                Filter = new TextFilter {DoesNotContain = valueExpression.Value}
            },
            _ => throw new NotImplementedException($"Unimplemented filter type: {filter.GetType().FullName}")
        };
    }

    public void Delete()
    {
        _deleted = true;
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
        var databaseContentsRequest = new SearchDatabaseRequest {DatabaseId = Definition.Id.RemoveDashes()};
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
            _logger.LogTrace("Database: '{DatabaseId}' has no entries", Definition.Id);
            return;
        }

        var databaseContents = databaseContentsResponse.Value;
        var resultsFromNotionApi = databaseContents.Results.Select(no => (PageObject) no);
        if (fullUpdate)
        {
            _logger.LogDebug("Performing full update from Notion for database: '{DatabaseTitle}' ({DatabaseId})",
                Definition.Title, Definition.Id);
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

    internal void UpdateDefinition(DatabaseDefinition databaseDefinition)
    {
        if (databaseDefinition.Id != Definition.Id)
            throw new Exception(
                $"Database definition cannot be updated because id's differ: {databaseDefinition.Id} != {Definition.Id}");

        Definition = databaseDefinition;
    }
}