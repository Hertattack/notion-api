using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.Query;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal interface IExecutionPlanBuilder
{
    IQueryPlan BuildFor(IQuery query, Metamodel metamodelStoreMetamodel);
}