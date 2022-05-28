using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Query;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal interface IExecutionPlanBuilder
{
    ExecutionPlan BuildFor(IQuery query, Metamodel metamodelStoreMetamodel);
}