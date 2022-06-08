﻿using NotionApi.Rest.Response.Database;
using NotionApi.Rest.Response.Database.Properties;
using NotionApi.Rest.Response.Page;
using Util.Extensions;

namespace NotionGraphDatabase.Storage.DataModel;

public class Database : IDataStoreObject
{
    private bool _deleted = false;

    private DataStore? _store;

    private readonly string _databaseId;

    private DatabaseObject? _notionRepresentation;

    private Dictionary<string, DatabasePage> _pages = new();

    private List<PropertyDefinition> _properties = new();

    public IEnumerable<PropertyDefinition> Properties =>
        _properties.AsReadOnly();

    public Database(DataStore? store, string databaseId)
    {
        _store = store;
        _databaseId = databaseId;
    }

    public IList<DatabasePage> Pages => _pages.Values.ToList();

    public void UpdateDefinition(DatabaseObject notionRepresentation)
    {
        if (_deleted)
            throw new StorageException("Updating deleted definition.");

        _notionRepresentation = notionRepresentation;
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

    public void FullUpdate(IEnumerable<PageObject> allPages)
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

        if (_pages.Count == 0)
        {
            _pages = all.ToDictionary(p => p.Id, p => new DatabasePage(this, p));
            return;
        }

        foreach (var updatedPage in all)
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
        }
    }

    public void Delete()
    {
        _deleted = true;

        _store = null;
        _notionRepresentation = null;
    }

    public string GetLastKnowEditTimestamp()
    {
        if (_pages.Count == 0)
            return "";

        return _pages.Max(p => p.Value.LastEditTimestamp).ToLongDateString();
    }

    public bool HasPages()
    {
        return _pages.Count > 0;
    }
}