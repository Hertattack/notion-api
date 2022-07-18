namespace NotionGraphDatabase.Util;

public static class StringExtensions
{
    public static string RemoveDashes(this string value)
    {
        return value.Replace("-", "");
    }

    public static string AddDashes(this string value)
    {
        try
        {
            var guid = Guid.Parse(value);
            return guid.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception($"Cannot add dashes to non-guid string: {value}", ex);
        }
    }
}