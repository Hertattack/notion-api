namespace NotionGraphDatabase.Util;

public static class StringExtensions
{
    public static string RemoveDashes(this string value)
    {
        return value.Replace("-", "");
    }
}