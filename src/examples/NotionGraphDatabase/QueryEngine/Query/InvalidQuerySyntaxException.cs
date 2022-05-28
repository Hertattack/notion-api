namespace NotionGraphDatabase.QueryEngine.Query;

public class InvalidQuerySyntaxException : Exception
{
    public InvalidQuerySyntaxException(string message) : base(message)
    {
    }
}