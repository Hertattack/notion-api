using System.Collections.Generic;
using NotionApi.Rest.Database;
using NotionApi.Rest.Objects;
using Util;

namespace NotionApi.Cache
{
    public interface INotionCache
    {
        void Update(IList<NotionObject> notionObjects);
        IEnumerable<ICacheMiss> CacheMisses { get; }

        Option<DatabaseObject> GetDatabase(string databaseId);
    }
}