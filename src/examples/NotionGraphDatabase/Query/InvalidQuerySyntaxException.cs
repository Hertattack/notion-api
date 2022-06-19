namespace NotionGraphDatabase.Query;

public class InvalidQuerySyntaxException : Exception
{
    public InvalidQuerySyntaxException(string message) : base(message)
    {
    }
}