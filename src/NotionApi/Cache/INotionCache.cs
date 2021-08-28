using System.Collections.Generic;
using NotionApi.Rest.Response.Database;
using NotionApi.Rest.Response.Objects;
using Util;

namespace NotionApi.Cache
{
    public interface INotionCache
    {
        void Update(IEnumerable<NotionObject> notionObjects);
        IEnumerable<ICacheMiss> CacheMisses { get; }

        Option<DatabaseObject> GetDatabase(string databaseId);
    }
}