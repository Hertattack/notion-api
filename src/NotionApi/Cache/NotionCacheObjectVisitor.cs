using NotionApi.Rest.Database;
using NotionApi.Rest.Objects;
using NotionApi.Rest.Page;
using Util.Visitor;

namespace NotionApi.Cache
{
    internal class NotionCacheObjectVisitor : TypedVisitor<NotionObject>
    {
        private readonly NotionCache _notionCache;

        public NotionCacheObjectVisitor(NotionCache notionCache)
        {
            _notionCache = notionCache;
        }

        protected override void Visit(VisitPath path, NotionObject obj)
        {
            switch (obj)
            {
                case PageObject page:
                    _notionCache.RegisterPage(page);
                    break;
                case DatabaseObject database:
                    _notionCache.RegisterDatabase(database);
                    break;
                default:
                    break;
            }
        }
    }
}