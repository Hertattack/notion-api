using System.Collections.Generic;
using NotionApi.Rest.Objects;

namespace NotionApi.Cache
{
    public interface INotionCache
    {
        void Refresh(IList<NotionObject> notionObjects);
        void UpdateNotionObjects(IList<NotionObject> results);
    }
}