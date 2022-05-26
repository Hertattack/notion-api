using Microsoft.Extensions.Logging;
using NotionGraphDatabase.QueryEngine.Model;
using NotionGraphDatabase.QueryEngine.Query;

namespace NotionGraphDatabase.QueryEngine;

internal class QueryEngineImplementation : IQueryEngine
{
    private readonly IQueryParser _queryParser;
    private readonly IQueryBuilder _queryBuilder;
    private readonly ILogger<QueryEngineImplementation> _logger;

    public QueryEngineImplementation(
        IQueryParser queryParser,
        IQueryBuilder queryBuilder,
        ILogger<QueryEngineImplementation> logger)
    {
        _queryParser = queryParser;
        _queryBuilder = queryBuilder;
        _logger = logger;
    }

    public IQuery Parse(string queryText)
    {
        _logger.LogDebug("Parsing query '{Query}'", queryText);

        if (_queryParser.Parse(queryText) is not QueryExpression ast)
            throw new QueryParseException(
                $"Could not parse the query: '{queryText}'. The query is not a query expression.");

        var query = _queryBuilder.FromAst(ast);

        _logger.LogDebug("Parsing finished");

        return query;
    }
}