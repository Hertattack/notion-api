using System.Collections.Generic;
using System.Linq;
using NotionApi.Rest.Response.Text;
using Util;

namespace NotionApi.Extensions;

public static class RichTextExtensions
{
    public static string ToPlainTextString(this IList<RichTextObject> items)
    {
        return items.Count > 0
            ? string.Join(" ", items.Select(i => i.PlainText))
            : string.Empty;
    }

    public static string ToPlainTextString(this Option<IList<RichTextObject>> optionalItems)
    {
        return optionalItems.HasValue
            ? optionalItems.Value.ToPlainTextString()
            : string.Empty;
    }
}