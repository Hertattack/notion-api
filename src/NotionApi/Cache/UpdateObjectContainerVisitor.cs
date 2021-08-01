using Microsoft.Extensions.Logging;
using NotionApi.Rest.Database;
using NotionApi.Rest.Objects;
using NotionApi.Rest.Page;
using NotionApi.Rest.Reference;
using Util;
using Util.Visitor;

namespace NotionApi.Cache
{
    internal class UpdateObjectContainerVisitor : TypedVisitor<NotionObject>
    {
        private readonly ILogger _logger;
        private readonly NotionCache _notionCache;

        public UpdateObjectContainerVisitor(ILogger logger, NotionCache notionCache)
        {
            _logger = logger;
            _notionCache = notionCache;
        }

        protected override void Visit(VisitPath path, NotionObject obj)
        {
            switch (obj)
            {
                case PageObject page:
                    UpdatePageContainer(path, page);
                    break;
                case DatabaseObject database:
                    UpdateDatabaseContainer(path, database);
                    break;
            }
        }

        private void UpdateDatabaseContainer(VisitPath path, DatabaseObject database)
        {
            database.Container = Option.None;

            var parent = database.Parent;
            if (!(parent is ParentPageReference pageReference))
                return;

            var parentPage = _notionCache.GetPage(pageReference.PageId);
            if (!parentPage.HasValue)
            {
                _logger.LogWarning("Could not find page with id: {PageId} for database: {Path}", pageReference.PageId, path.ToString());
                return;
            }

            database.Container = parentPage.Value;
        }

        private void UpdatePageContainer(VisitPath path, PageObject page)
        {
            page.Container = Option.None;

            var parent = page.Parent;

            switch (parent)
            {
                case ParentPageReference pageReference:
                    UpdatePageContainer(path, page, pageReference);
                    break;
                case ParentDatabaseReference databaseReference:
                    UpdatePageContainer(path, page, databaseReference);
                    break;
            }
        }

        private void UpdatePageContainer(VisitPath path, PageObject page, ParentDatabaseReference databaseReference)
        {
            var parentDatabase = _notionCache.GetDatabase(databaseReference.DatabaseId);
            if (!parentDatabase.HasValue)
            {
                _logger.LogWarning("Could not find database with id: {PageId} for page: {Path}", databaseReference.DatabaseId, path.ToString());
                return;
            }

            page.Container = parentDatabase.Value;
        }

        private void UpdatePageContainer(VisitPath path, PageObject page, ParentPageReference pageReference)
        {
            var parentPage = _notionCache.GetPage(pageReference.PageId);
            if (!parentPage.HasValue)
            {
                _logger.LogWarning("Could not find page with id: {PageId} for page: {Path}", pageReference.PageId, path.ToString());
                return;
            }

            page.Container = parentPage.Value;
        }
    }
}