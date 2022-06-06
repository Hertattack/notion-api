using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.Storage;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal class CreateResultStep : ExecutionPlanStep
{
    private readonly Dictionary<string, ReturnMapping> _mappings;

    public CreateResultStep(IEnumerable<ReturnMapping> mappings)
    {
        _mappings = mappings.ToDictionary(m => m.Alias);
    }

    public override void Execute(QueryExecutionContext context, IStorageBackend storageBackend)
    {
        var resultContext = context.GetCurrentResultContext();

        if (resultContext is null)
            return;
        
        var resultSet = new List<Dictionary<string, object>>();
        foreach (var row in resultContext.DenormalizeRows())
        {
            var resultRow = new Dictionary<string, object>();
            
        }
    }
}