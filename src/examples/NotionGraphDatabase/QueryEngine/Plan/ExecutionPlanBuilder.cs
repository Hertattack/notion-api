using Microsoft.Extensions.Logging;
using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Plan.Steps;
using NotionGraphDatabase.QueryEngine.Query;
using NotionGraphDatabase.QueryEngine.Query.Filter;
using NotionGraphDatabase.QueryEngine.Validation;
using NotionGraphDatabase.Storage;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal class ExecutionPlanBuilder : IExecutionPlanBuilder
{
    private readonly IQueryValidator _queryValidator;
    private readonly IStorageBackend _storageBackend;
    private readonly ILogger<ExecutionPlanBuilder> _logger;

    public ExecutionPlanBuilder(
        IQueryValidator queryValidator,
        IStorageBackend storageBackend,
        ILogger<ExecutionPlanBuilder> logger)
    {
        _queryValidator = queryValidator;
        _storageBackend = storageBackend;
        _logger = logger;
    }

    public IQueryPlan BuildFor(IQuery query, Metamodel metamodel)
    {
        _logger.LogDebug("Build execution plan for query: {Query}", query.ToString());

        var validationResult = _queryValidator.Validate(query, metamodel);

        if (validationResult.IsInvalid)
            throw new InvalidQueryException(validationResult);

        var plan = Analyze(query, metamodel);

        _logger.LogDebug("Execution plan built");
        return plan;
    }

    private IQueryPlan Analyze(IQuery query, Metamodel metamodel)
    {
        var databases = metamodel.Databases.ToDictionary(d => d.Alias);
        var plan = new ExecutionPlan(query, metamodel);

        var filtersPerDatabase = AggregateFiltersByDatabase(query);
        foreach (var filterExpressionsPerDatabase in filtersPerDatabase)
        {
            var database = databases[filterExpressionsPerDatabase.Key];

            if (filtersPerDatabase.Count > 0)
            {
                var expressionBuilder = new FilterExpressionBuilder();
                foreach (var expressions in filterExpressionsPerDatabase.Value)
                    expressionBuilder.Or(FilterExpressionBuilder.And(expressions));

                var expression = expressionBuilder.Build();
                if (_storageBackend.Supports(expression))
                {
                    var step = new FilteredFetchDatabaseStep(database, expression);
                    plan.AddStep(step);
                    continue;
                }
            }

            plan.AddStep(new FetchDatabaseStep(database));
        }

        foreach (var selectStepContext in query.SelectSteps)
        {
            var currentStep = selectStepContext.Step;
            var database = databases[currentStep.AssociatedNode.NodeName];

            if (selectStepContext.PreviousStepContext is not null)
            {
                var role = currentStep.Role;
                var relationalFilterStep = new SelectNodeViaRelationStep(
                    role, database, currentStep.AssociatedNode.Alias,
                    FilterExpressionBuilder.And(currentStep.Filter).Build());
                plan.AddStep(relationalFilterStep);
            }
            else
            {
                plan.AddStep(new SelectFromNodeStep(database, currentStep.AssociatedNode.Alias,
                    FilterExpressionBuilder.And(currentStep.Filter).Build()));
            }
        }

        var returnPropertyMappings = query.ReturnPropertySelections
            .Select(rps => CreateReturnMapping(databases[rps.NodeReference.NodeName], rps)).ToList();

        plan.AddStep(new CreateResultStep(returnPropertyMappings));

        return plan;
    }

    private static Dictionary<string, List<List<FilterExpression>>> AggregateFiltersByDatabase(IQuery query)
    {
        var result = new Dictionary<string, List<List<FilterExpression>>>();
        foreach (var stepContext in query.SelectSteps)
        {
            var stepNodeName = stepContext.Step.AssociatedNode.NodeName;
            if (!result.ContainsKey(stepNodeName)) result[stepNodeName] = new List<List<FilterExpression>>();

            var aliases = stepContext.Path
                .ToDictionary(p => p.Step.AssociatedNode.Alias, p => p.Step.AssociatedNode.NodeName);

            foreach (var groupedFilters in stepContext.Step.Filter.GroupBy(f => f.Alias))
            {
                var nodeName = aliases[groupedFilters.Key];
                var filterExpressions = groupedFilters.ToList();

                if (nodeName == stepNodeName || result.ContainsKey(nodeName))
                    result[nodeName].Add(filterExpressions);
                else
                    result[nodeName] = new List<List<FilterExpression>> {filterExpressions};
            }
        }

        return result;
    }

    private static ReturnMapping CreateReturnMapping(Database database, NodeReturnPropertySelection selection)
    {
        var nodeReference = selection.NodeReference;
        var alias = nodeReference.Alias;

        return selection.PropertySelection switch
        {
            NodeAllPropertiesSelected =>
                new ReturnMapping(database, alias) {AllSelected = true},

            NodeSpecificPropertiesSelected specificPropertiesSelected =>
                new ReturnMapping(database, alias, specificPropertiesSelected.PropertyNames),

            _ =>
                throw new QueryPlanGenerationException(
                    $"Unsupported type of property selection: '{selection.GetType()}'")
        };
    }
}