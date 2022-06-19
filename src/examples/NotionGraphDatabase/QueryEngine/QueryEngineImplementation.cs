using Microsoft.Extensions.Logging;
using NotionGraphDatabase.Interface;
using NotionGraphDatabase.Interface.Result;
using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Ast;
using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.QueryEngine.Plan;
using NotionGraphDatabase.QueryEngine.Query;
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
        IExecutionPlanBuilder executionPlanBuilder,
        IStorageBackend storageBackend,
        ILogger<QueryEngineImplementation> logger)
    {
        if (metamodelStore.Metamodel is null)
            throw new Exception("No metamodel available for querying.");

        _metamodelStore = metamodelStore;
        _queryParser = queryParser;
        _queryBuilder = queryBuilder;
        _executionPlanBuilder = executionPlanBuilder;
        _storageBackend = storageBackend;
        _logger = logger;
    }

    private IQuery Parse(string queryText)
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
        _logger.LogDebug("Executing query: {Query}", queryText);

        var query = Parse(queryText);

        var plan = _executionPlanBuilder.BuildFor(query, _metamodelStore.Metamodel);

        var queryResult = ExecutePlan(plan);

        _logger.LogDebug("Query finished");
        return queryResult;
    }

    private QueryResult ExecutePlan(IQueryPlan plan)
    {
        var context = new QueryExecutionContext(plan.Metamodel);

        _logger.LogDebug("Executing query plan");
        foreach (var step in plan.Steps)
        {
            _logger.LogDebug("Executing step: {ExecutionPlanStep}", step.ToString());
            step.Execute(context, _storageBackend);
        }

        var result = new QueryResult(plan.Query, plan.Metamodel, context.ResultSet);
        return result;
    }
}