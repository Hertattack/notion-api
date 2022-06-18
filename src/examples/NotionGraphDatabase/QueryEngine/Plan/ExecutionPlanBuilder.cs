using Microsoft.Extensions.Logging;
using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Plan.Steps;
using NotionGraphDatabase.QueryEngine.Query;
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

    private IQueryPlan Analyze(IQuery query, Metamodel metamodel)
    {
        var databases = metamodel.Databases.ToDictionary(d => d.Alias);
        var plan = new ExecutionPlan(query, metamodel);

        foreach (var nodeReference in query.NodeReferences)
        {
            var database = databases[nodeReference.NodeName];
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