using System.Collections.Generic;
using NotionApi.Rest.Objects;

namespace NotionApi.Cache
{
    public interface INotionCache
    {
        void UpdateObjects(IList<NotionObject> notionObjects);
        IEnumerable<ICacheMiss> CacheMisses { get; }
    }
}