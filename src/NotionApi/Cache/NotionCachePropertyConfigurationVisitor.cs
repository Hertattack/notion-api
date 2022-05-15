using Microsoft.Extensions.Logging;
using NotionApi.Rest.Response.Database;
using NotionApi.Rest.Response.Database.Properties;
using Util.Visitor;

namespace NotionApi.Cache;

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
        Order = 20;
    }

    protected override void Visit(VisitPath path, NotionPropertyConfiguration obj)
    {
        if (!path.Previous.HasValue)
            return;

        var optionalDatabaseObject = path.FindPrevious<DatabaseObject>();

        if (!optionalDatabaseObject.HasValue)
        {
            _logger.LogWarning(
                "Unexpected path for property configuration. Did not find database that contains the property following path: {Path}",
                path.ToString());
            return;
        }

        if (!obj.Container.HasValue)
            obj.Container = optionalDatabaseObject.Value;

        if (obj is RelationPropertyConfiguration relationPropertyConfiguration)
            _notionCache.RegisterPropertyConfiguration(
                relationPropertyConfiguration.Configuration.DatabaseId,
                obj,
                relationPropertyConfiguration.Configuration.SyncedPropertyId);

        _notionCache.RegisterPropertyConfiguration(optionalDatabaseObject.Value.Id, obj);
    }
}