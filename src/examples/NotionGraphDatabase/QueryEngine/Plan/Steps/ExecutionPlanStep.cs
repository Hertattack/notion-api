using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.Storage;

namespace NotionGraphDatabase.QueryEngine.Plan.Steps;

internal abstract class ExecutionPlanStep : IExecutionPlanStep
{
    public abstract void Execute(QueryExecutionContext executionContext, IStorageBackend storageBackend);
}