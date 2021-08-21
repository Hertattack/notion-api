using System.Collections.Generic;
using NotionApi.Rest.Objects;

namespace NotionApi.Cache
{
    public interface INotionCache
    {
        void Update(IList<NotionObject> notionObjects);
        IEnumerable<ICacheMiss> CacheMisses { get; }
    }
}