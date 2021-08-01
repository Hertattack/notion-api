using Microsoft.Extensions.Logging;
using NotionApi.Rest.Database;
using NotionApi.Rest.Database.Properties;
using Util.Visitor;

namespace NotionApi.Cache
{
    internal class NotionCachePropertyConfigurationVisitor : TypedVisitor<NotionPropertyConfiguration>
    {
        private readonly ILogger _logger;
        private readonly NotionCache _notionCache;

        public NotionCachePropertyConfigurationVisitor(
            ILogger logger,
            NotionCache notionCache)
        {
            _logger = logger;
            _notionCache = notionCache;
        }

        protected override void Visit(VisitPath path, NotionPropertyConfiguration obj)
        {
            if (!path.Previous.HasValue)
                return;

            var target = path.Previous.Value.Target;

            if (!(target is DatabaseObject database))
            {
                _logger.LogWarning("Unexpected path for property configuration. Did not find database that contains the property following path: {Path}",
                    path.ToString());
                return;
            }

            _notionCache.RegisterPropertyConfiguration(database.Id, obj);
        }
    }
}