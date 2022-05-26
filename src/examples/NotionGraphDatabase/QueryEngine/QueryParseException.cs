using sly.parser;

namespace NotionGraphDatabase.QueryEngine;

public class QueryParseException : Exception
{
    private readonly List<ParseError>? _resultErrors = null;

    public QueryParseException(string message) : base(message)
    {
    }

    public QueryParseException(List<ParseError>? resultErrors) : base("Parse errors.")
    {
        _resultErrors = resultErrors;
    }
}