using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using NotionApi.Rest.Response.Database;
using NotionApi.Rest.Response.Database.Properties;
using NotionApi.Rest.Response.Objects;
using NotionApi.Rest.Response.Page;
using Util;
using Util.Visitor;

namespace NotionApi.Cache;

public class NotionCache : INotionCache
{
    private readonly IObjectVisitorFactory _objectVisitorFactory;

    private readonly Dictionary<string, DatabaseObject> _databases = new();
    private readonly Dictionary<string, PageObject> _pages = new();

    private readonly Dictionary<string, Dictionary<string, NotionPropertyConfiguration>> _propertyConfigurations =
        new();

    private readonly Dictionary<string, ObjectType> _ids = new();

    private readonly List<ICacheMiss> _cacheCacheMisses = new();

    public IEnumerable<ICacheMiss> CacheMisses => _cacheCacheMisses;

    private readonly IVisitor _objectVisitor;
    private readonly IVisitor _updateObjectVisitor;
    private readonly IVisitor _propertyConfigurationVisitor;
    private readonly IVisitor _updatePropertyValueVisitor;

    public NotionCache(
        IObjectVisitorFactory objectVisitorFactory,
        ILoggerFactory loggerFactory)
    {
        _objectVisitorFactory = objectVisitorFactory;

        _objectVisitor = new NotionCacheObjectVisitor(this);

        var logger = loggerFactory.CreateLogger(typeof(UpdateObjectContainerVisitor));
        _updateObjectVisitor = new UpdateObjectContainerVisitor(logger, this);

        logger = loggerFactory.CreateLogger(typeof(NotionCachePropertyConfigurationVisitor));
        _propertyConfigurationVisitor = new NotionCachePropertyConfigurationVisitor(logger, this);

        logger = loggerFactory.CreateLogger(typeof(UpdatePropertyValueConfigurationVisitor));
        _updatePropertyValueVisitor = new UpdatePropertyValueConfigurationVisitor(logger, this);
    }

    public void Update(IEnumerable<NotionObject> notionObjects)
    {
        Clear();

        var visitor = _objectVisitorFactory.CreateFor(notionObjects,
            _objectVisitor,
            _propertyConfigurationVisitor,
            _updateObjectVisitor,
            _updatePropertyValueVisitor);

        visitor.VisitAll();
    }

    private void Clear()
    {
        _cacheCacheMisses.Clear();
        _databases.Clear();
        _pages.Clear();
        _ids.Clear();
        _propertyConfigurations.Clear();
    }

    public void RegisterPage(PageObject page)
    {
        if (!_pages.ContainsKey(page.Id))
            _ids.Add(page.Id, ObjectType.Page);

        _pages[page.Id] = page;
    }

    public void RegisterDatabase(DatabaseObject database)
    {
        if (!_databases.ContainsKey(database.Id))
            _ids.Add(database.Id, ObjectType.Database);

        _databases[database.Id] = database;
    }

    public void RegisterPropertyConfiguration(string databaseId, NotionPropertyConfiguration propertyConfiguration)
    {
        RegisterPropertyConfiguration(databaseId, propertyConfiguration, Option.None);
    }

    public void RegisterPropertyConfiguration(string databaseId, NotionPropertyConfiguration propertyConfiguration,
        Option<string> propertyId)
    {
        if (!propertyConfiguration.Id.HasValue && !propertyId.HasValue)
            return;

        if (!_propertyConfigurations.ContainsKey(databaseId))
            _propertyConfigurations.Add(databaseId, new Dictionary<string, NotionPropertyConfiguration>());

        var id = propertyId.HasValue ? propertyId.Value : propertyConfiguration.Id.Value;
        _propertyConfigurations[databaseId][id] = propertyConfiguration;
    }

    public Option<PageObject> GetPage(string pageId)
    {
        if (_pages.ContainsKey(pageId))
            return _pages[pageId];

        if (!_cacheCacheMisses.Any(m => m is ObjectReferenceCacheMiss {Type: "page"} objRef && objRef.Id == pageId))
            _cacheCacheMisses.Add(new ObjectReferenceCacheMiss("page", pageId));

        return Option.None;
    }

    public Option<DatabaseObject> GetDatabase(string databaseId)
    {
        if (_databases.ContainsKey(databaseId))
            return _databases[databaseId];

        if (!_cacheCacheMisses.Any(m =>
                m is ObjectReferenceCacheMiss {Type: "database"} objRef && objRef.Id == databaseId))
            _cacheCacheMisses.Add(new ObjectReferenceCacheMiss("database", databaseId));

        return Option.None;
    }

    public Option<NotionPropertyConfiguration> GetPropertyConfiguration(string databaseId, string propertyId)
    {
        if (!_propertyConfigurations.ContainsKey(databaseId))
        {
            var db = GetDatabase(databaseId);
            if (!_cacheCacheMisses.Any(m =>
                    m is PropertyConfigurationCacheMiss propConfigRef
                    && propConfigRef.DatabaseId == databaseId
                    && propConfigRef.PropertyId == propertyId))
                _cacheCacheMisses.Add(new PropertyConfigurationCacheMiss(databaseId, propertyId, db.HasValue));

            return Option.None;
        }

        var databaseProperties = _propertyConfigurations[databaseId];
        if (!databaseProperties.ContainsKey(propertyId))
        {
            if (!_cacheCacheMisses.Any(m =>
                    m is PropertyConfigurationCacheMiss propConfigRef
                    && propConfigRef.DatabaseId == databaseId
                    && propConfigRef.PropertyId == propertyId))
                _cacheCacheMisses.Add(new PropertyConfigurationCacheMiss(databaseId, propertyId, true));

            return Option.None;
        }


        return databaseProperties[propertyId];
    }
}