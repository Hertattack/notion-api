using System.Collections.Generic;
using System.Linq;
using NotionApi.Rest.Response.Objects;

namespace NotionApi.Util;

public static class NotionObjectExtensions
{
    public static IEnumerable<NotionObject> Deduplicate(this IEnumerable<NotionObject> notionObjects)
    {
        var identifiers = new HashSet<string>();

        return notionObjects.Where(n =>
        {
            if (identifiers.Contains(n.Id))
                return false;

            identifiers.Add(n.Id);

            return true;
        });
    }
}