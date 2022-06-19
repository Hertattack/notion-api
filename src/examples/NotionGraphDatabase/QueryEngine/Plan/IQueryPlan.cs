using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.Query;
using NotionGraphDatabase.QueryEngine.Plan.Steps;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal interface IQueryPlan
{
    Metamodel Metamodel { get; }
    IEnumerable<IExecutionPlanStep> Steps { get; }
    IQuery Query { get; }
}