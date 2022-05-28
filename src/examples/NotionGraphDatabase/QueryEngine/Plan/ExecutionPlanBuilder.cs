using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Query;
using NotionGraphDatabase.QueryEngine.Validation;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal class ExecutionPlanBuilder : IExecutionPlanBuilder
{
    private readonly IQueryValidator _queryValidator;

    public ExecutionPlanBuilder(IQueryValidator queryValidator)
    {
        _queryValidator = queryValidator;
    }

    public ExecutionPlan BuildFor(IQuery query, Metamodel metamodel)
    {
        var validationResult = _queryValidator.Validate(query, metamodel);

        if (validationResult.IsInvalid)
            throw new InvalidQueryException(validationResult);

        var plan = new ExecutionPlan(query, metamodel);
        return plan;
    }
}