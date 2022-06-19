using Microsoft.Extensions.Logging;
using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Plan.Steps;
using NotionGraphDatabase.QueryEngine.Query;
using NotionGraphDatabase.QueryEngine.Query.Expression;
using NotionGraphDatabase.QueryEngine.Query.Filter;
using NotionGraphDatabase.QueryEngine.Validation;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal class ExecutionPlanBuilder : IExecutionPlanBuilder
{
    private readonly IQueryValidator _queryValidator;
    private readonly ILogger<ExecutionPlanBuilder> _logger;

    public ExecutionPlanBuilder(
        IQueryValidator queryValidator,
        ILogger<ExecutionPlanBuilder> logger)
    {
        _queryValidator = queryValidator;
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

    private static IQueryPlan Analyze(IQuery query, Metamodel metamodel)
    {
        var databases = metamodel.Databases.ToDictionary(d => d.Alias);
        var plan = new ExecutionPlan(query, metamodel);

        var filtersPerDatabase = AggregateFiltersByDatabase(query);
        foreach (var filterExpressionsPerDatabase in filtersPerDatabase)
        {
            var database = databases[filterExpressionsPerDatabase.Key];
            if (CanPushDown(filterExpressionsPerDatabase.Value))
            {
                var step = new FilteredFetchDatabaseStep(database);

                foreach (var expressions in filterExpressionsPerDatabase.Value)
                    step.AddOrCondition(expressions);

                plan.AddStep(step);
            }
            else
            {
                plan.AddStep(new FetchDatabaseStep(database));
            }
        }

        foreach (var selectStepContext in query.SelectSteps)
        {
            var currentStep = selectStepContext.Step;
            var database = databases[currentStep.AssociatedNode.NodeName];

            if (selectStepContext.PreviousStepContext is not null)
            {
                var role = currentStep.Role;
                var relationalFilterStep = new SelectNodeViaRelationStep(
                    role, database,
                    currentStep.AssociatedNode.Alias, currentStep.Filter);
                plan.AddStep(relationalFilterStep);
            }
            else
            {
                plan.AddStep(new SelectFromNodeStep(database, currentStep.AssociatedNode.Alias, currentStep.Filter));
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
        foreach (var stepContext in query.SelectSteps.Where(s => s.Step.HasFilter))
        {
            var aliases = stepContext.Path
                .ToDictionary(p => p.Step.AssociatedNode.Alias, p => p.Step.AssociatedNode.NodeName);

            foreach (var groupedFilters in stepContext.Step.Filter.GroupBy(f => f.Alias))
            {
                var nodeName = aliases[groupedFilters.Key];
                var filterExpressions = groupedFilters.ToList();

                if (result.ContainsKey(nodeName))
                    result[nodeName].Add(filterExpressions);
                else
                    result[nodeName] = new List<List<FilterExpression>> {filterExpressions};
            }
        }

        return result;
    }

    private static bool CanPushDown(IEnumerable<List<FilterExpression>> expressions)
    {
        return expressions.All(l => l.All(CanPushDown));
    }

    private static bool CanPushDown(FilterExpression filterExpression)
    {
        return filterExpression.Expression is not PropertyValueCompareExpression;
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