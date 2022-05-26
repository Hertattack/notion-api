using sly.parser;

namespace NotionGraphDatabase.QueryEngine;

public class QueryParseException : Exception
{
    public override string Message { get; }

    public List<ParseError> ParseErrors { get; }

    public QueryParseException(string message)
    {
        Message = message;
        ParseErrors = new List<ParseError>();
    }

    public QueryParseException(IEnumerable<ParseError> resultErrors)
    {
        var parseErrors = resultErrors.ToList();
        ParseErrors = parseErrors;
        Message = string.Join(", ",
            parseErrors.Select(e => $"{e.ErrorMessage} - on line: {e.Line} column: {e.Column}"));
    }
}