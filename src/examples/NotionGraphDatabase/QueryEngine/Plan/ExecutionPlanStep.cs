using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.Storage;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal abstract class ExecutionPlanStep : IExecutionPlanStep
{
    public abstract void Execute(QueryExecutionContext executionContext, IStorageBackend storageBackend);
}