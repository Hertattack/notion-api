namespace NotionGraphDatabase.Query.Parser;

internal class QuerySyntaxException : Exception
{
    public QuerySyntaxException(string message) : base(message)
    {
    }
}