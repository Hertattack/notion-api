using Microsoft.Extensions.Logging;
using NotionApi.Rest.Database;
using NotionApi.Rest.Page;
using NotionApi.Rest.Page.Properties;
using Util;
using Util.Visitor;

namespace NotionApi.Cache
{
    internal class UpdatePropertyValueConfigurationVisitor : TypedVisitor<NotionPropertyValue>
    {
        private readonly ILogger _logger;
        private readonly NotionCache _notionCache;

        public UpdatePropertyValueConfigurationVisitor(ILogger logger, NotionCache notionCache)
        {
            _logger = logger;
            _notionCache = notionCache;
        }

        protected override void Visit(VisitPath path, NotionPropertyValue propertyValue)
        {
            propertyValue.Configuration = Option.None;
            if (!path.Previous.HasValue || !propertyValue.Id.HasValue)
                return;

            var parent = path.Previous.Value.Target;
            if (!(parent is PageObject pageObject))
            {
                _logger.LogWarning("Parent for property value with id: {PropertyId} is not a page, this is not expected. Path: {Path}",
                    propertyValue.Id, path.ToString());
                return;
            }

            propertyValue.Container = pageObject;

            if (!pageObject.Container.HasValue)
            {
                _logger.LogWarning("Container for page: {PageId} is not set", pageObject.Id);
                return;
            }

            if (!(pageObject.Container.Value is DatabaseObject parentDatabase))
            {
                _logger.LogWarning("Container for page: {PageId} is not a database, yet it has properties. This is not expected", pageObject.Id);
                return;
            }

            var propertyConfigurationId = propertyValue.Id.Value;
            var propertyConfiguration = _notionCache.GetPropertyConfiguration(parentDatabase.Id, propertyConfigurationId);
            if (!propertyConfiguration.HasValue)
            {
                _logger.LogWarning(
                    "Could not find property configuration for id: {PropertyConfigurationId}, in database: {DatabaseId} for page: {PageId}",
                    propertyConfigurationId, parentDatabase.Id, pageObject.Id);
                return;
            }

            propertyValue.Configuration = propertyConfiguration.Value;
        }
    }
}