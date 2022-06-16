using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.Storage;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal interface IExecutionPlanStep
{
    void Execute(QueryExecutionContext context, IStorageBackend storageBackend);
}