using NotionGraphDatabase.Query.Parser;
using NotionGraphDatabase.Query.Parser.Ast;
using sly.parser;
using sly.parser.generator;

namespace NotionGraphDatabase.QueryEngine;

internal class NotionQueryParser : IQueryParser
{
    private readonly Parser<QueryToken, QueryPredicate> _queryParser;

    public NotionQueryParser()
    {
        var parser = new QueryParser();
        var builder = new ParserBuilder<QueryToken, QueryPredicate>();
        var queryParser = builder.BuildParser(parser, ParserType.LL_RECURSIVE_DESCENT, "query");

        if (queryParser.IsError)
            throw new Exception(
                $"Could not build query parser. Errors: {queryParser.Errors.Select(e => e.ToString())}");

        _queryParser = queryParser.Result;
    }

    public QueryPredicate Parse(string query)
    {
        var result = _queryParser.Parse(query);

        if (result.IsError)
            throw new QueryParseException(result.Errors);

        return result.Result;
    }
}