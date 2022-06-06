using Microsoft.Extensions.Logging;
using NotionGraphDatabase.Interface;
using NotionGraphDatabase.Interface.Result;
using NotionGraphDatabase.QueryEngine.Ast;
using NotionGraphDatabase.QueryEngine.Plan;
using NotionGraphDatabase.QueryEngine.Query;
using NotionGraphDatabase.QueryEngine.Validation;
using NotionGraphDatabase.Storage;

namespace NotionGraphDatabase.QueryEngine;

internal class QueryEngineImplementation : IQueryEngine
{
    private readonly IMetamodelStore _metamodelStore;
    private readonly IQueryParser _queryParser;
    private readonly IQueryBuilder _queryBuilder;
    private readonly IExecutionPlanBuilder _executionPlanBuilder;
    private readonly IStorageBackend _storageBackend;
    private readonly ILogger<QueryEngineImplementation> _logger;

    public QueryEngineImplementation(
        IMetamodelStore metamodelStore,
        IQueryParser queryParser,
        IQueryBuilder queryBuilder,
        IQueryValidator queryValidator,
        IExecutionPlanBuilder executionPlanBuilder,
        IStorageBackend storageBackend,
        ILogger<QueryEngineImplementation> logger)
    {
        _metamodelStore = metamodelStore;
        _queryParser = queryParser;
        _queryBuilder = queryBuilder;
        _executionPlanBuilder = executionPlanBuilder;
        _storageBackend = storageBackend;
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

    public QueryResult Execute(string queryText)
    {
        var query = Parse(queryText);
        var plan = _executionPlanBuilder.BuildFor(query, _metamodelStore.Metamodel);
        return plan.Execute(_storageBackend);
    }
}