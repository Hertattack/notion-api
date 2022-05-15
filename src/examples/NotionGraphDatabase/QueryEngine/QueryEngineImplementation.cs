using NotionGraphDatabase.QueryEngine.Model;
using NotionGraphDatabase.QueryEngine.Parser;
using sly.parser;
using sly.parser.generator;

namespace NotionGraphDatabase.QueryEngine;

internal class QueryEngineImplementation : IQueryEngine
{
    private readonly Parser<QueryToken, IQueryAst> _queryParser;

    public QueryEngineImplementation()
    {
        var parser = new QueryParser();
        var builder = new ParserBuilder<QueryToken, IQueryAst>();
        var queryParser = builder.BuildParser(parser, ParserType.LL_RECURSIVE_DESCENT, "query");

        if (queryParser.IsError)
            throw new Exception($"Could not build query parser. Errors: {queryParser.Errors.Select(e => e.Message)}");

        _queryParser = queryParser.Result;
    }

    public QueryAbstractSyntaxTree Parse(string query)
    {
        var result = _queryParser.Parse(query);

        if (result.IsError)
            throw new QueryParseException(result.Errors);

        return (QueryAbstractSyntaxTree) result.Result;
    }
}