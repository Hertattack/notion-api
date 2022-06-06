using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.Storage;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal class RelationalFilterStep : ExecutionPlanStep
{
    private readonly string _role;
    private readonly Database _target;

    public RelationalFilterStep(string role, Database target)
    {
        _role = role;
        _target = target;
        throw new NotImplementedException();
    }

    public override void Execute(QueryExecutionContext context, IStorageBackend storageBackend)
    {
        throw new NotImplementedException();
    }
}