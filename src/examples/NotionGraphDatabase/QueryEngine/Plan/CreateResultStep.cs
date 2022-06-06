using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.Storage;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal class CreateResultStep : ExecutionPlanStep
{
    public override void Execute(QueryExecutionContext context, IStorageBackend storageBackend)
    {
    }
}