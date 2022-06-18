using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Plan.Steps;
using NotionGraphDatabase.QueryEngine.Query;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal interface IQueryPlan
{
    Metamodel Metamodel { get; }
    IEnumerable<IExecutionPlanStep> Steps { get; }
    IQuery Query { get; }
}