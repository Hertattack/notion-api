using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using NotionApi.Rest.Database;
using NotionApi.Rest.Database.Properties;
using NotionApi.Rest.Objects;
using NotionApi.Rest.Page;
using Util;
using Util.Visitor;

namespace NotionApi.Cache
{
    public class NotionCache : INotionCache
    {
        private readonly IObjectVisitorFactory _objectVisitorFactory;

        private readonly Dictionary<string, DatabaseObject> _databases = new Dictionary<string, DatabaseObject>();
        private readonly Dictionary<string, PageObject> _pages = new Dictionary<string, PageObject>();

        private readonly Dictionary<string, Dictionary<string, NotionPropertyConfiguration>> _propertyConfigurations =
            new Dictionary<string, Dictionary<string, NotionPropertyConfiguration>>();

        private readonly Dictionary<string, ObjectType> _ids = new Dictionary<string, ObjectType>();

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

        public void Refresh(IList<NotionObject> notionObjects)
        {
            Clear();
            var visitor = _objectVisitorFactory.CreateFor(notionObjects, _objectVisitor, _propertyConfigurationVisitor);
            visitor.VisitAll();
        }

        public void UpdateNotionObjects(IList<NotionObject> notionObjects)
        {
            var objectUpdateVisitor = _objectVisitorFactory.CreateFor(notionObjects, _updateObjectVisitor);
            objectUpdateVisitor.VisitAll();

            var propertyUpdateVisitor = _objectVisitorFactory.CreateFor(notionObjects, _updatePropertyValueVisitor);
            propertyUpdateVisitor.VisitAll();
        }

        private void Clear()
        {
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

        public void RegisterPropertyConfiguration(string databaseId, NotionPropertyConfiguration propertyConfiguration) =>
            RegisterPropertyConfiguration(databaseId, propertyConfiguration, Option.None);

        public void RegisterPropertyConfiguration(string databaseId, NotionPropertyConfiguration propertyConfiguration, Option<string> propertyId)
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

            return Option.None;
        }

        public Option<DatabaseObject> GetDatabase(string databaseId)
        {
            if (_databases.ContainsKey(databaseId))
                return _databases[databaseId];

            return Option.None;
        }

        public Option<NotionPropertyConfiguration> GetPropertyConfiguration(string databaseId, string propertyId)
        {
            if (!_propertyConfigurations.ContainsKey(databaseId))
                return Option.None;

            var databaseProperties = _propertyConfigurations[databaseId];
            if (!databaseProperties.ContainsKey(propertyId))
                return Option.None;

            return databaseProperties[propertyId];
        }
    }
}