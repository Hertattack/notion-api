using sly.parser;

namespace NotionGraphDatabase.QueryEngine;

public class QueryParseException : Exception
{
    private readonly List<ParseError> _resultErrors;

    public QueryParseException(List<ParseError> resultErrors)
    {
        _resultErrors = resultErrors;
    }
}