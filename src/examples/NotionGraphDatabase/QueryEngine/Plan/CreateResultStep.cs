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

        var resultSet = context.ResultSet;

        if (!_mappings.ContainsKey(resultContext.Alias)) return;

        var mapping = _mappings[resultContext.Alias];
        if (!mapping.AllSelected && !mapping.PropertyNames.Any()) return;

        foreach (var intermediateResultRow in resultContext.IntermediateResultRows)
        {
            var propertyNames = mapping.AllSelected ? intermediateResultRow.PropertyNames : mapping.PropertyNames;
            var resultRow = resultSet[intermediateResultRow.Id];
            foreach (var propertyName in propertyNames)
                resultRow[propertyName] = intermediateResultRow[propertyName];
        }
    }
}