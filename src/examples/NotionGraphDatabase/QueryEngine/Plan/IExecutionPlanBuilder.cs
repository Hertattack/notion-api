using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Query;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal interface IExecutionPlanBuilder
{
    IQueryPlan BuildFor(IQuery query, Metamodel metamodelStoreMetamodel);
}