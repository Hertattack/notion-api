using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.Storage;
using NotionGraphDatabase.Util;

namespace NotionGraphDatabase.QueryEngine.Plan.Steps;

internal class CreateResultStep : ExecutionPlanStep
{
    private readonly Dictionary<string, ReturnMapping> _mappings;

    public CreateResultStep(IEnumerable<ReturnMapping> mappings)
    {
        _mappings = mappings.ToDictionary(m => m.Alias);
    }

    public override void Execute(QueryExecutionContext executionContext, IStorageBackend storageBackend)
    {
        var resultContext = executionContext.GetCurrentResultContext();

        if (resultContext is null)
            return;

        var mapping = _mappings.ContainsKey(resultContext.Alias) ? _mappings[resultContext.Alias] : null;
        var resultSet = executionContext.ResultSet;

        foreach (var intermediateResultRow in resultContext.IntermediateResultRows)
        {
            var resultRow =
                new ResultRow(new CompositeKey(resultContext.Alias, intermediateResultRow.Id.RemoveDashes()));
            resultSet.AddRow(resultRow);

            if (mapping is not null)
                foreach (var propertyName in mapping.AllSelected
                             ? intermediateResultRow.PropertyNames
                             : mapping.PropertyNames)
                    resultRow[new FieldIdentifier(resultContext.Alias, propertyName)] =
                        intermediateResultRow[propertyName];

            AddParentRows(resultSet, resultRow, resultContext.ParentContext, intermediateResultRow.ParentRows);
        }
    }

    private void AddParentRows(
        ResultSet resultSet,
        ResultRow resultRow,
        IntermediateResultContext? parentContext,
        IEnumerable<IntermediateResultRow> parentRows)
    {
        if (parentContext is null)
            return;

        var mapping = _mappings.ContainsKey(parentContext.Alias) ? _mappings[parentContext.Alias] : null;

        var parentRowsList = parentRows.ToList();
        var lastIndex = parentRowsList.Count - 1;
        var denormalizedSet = parentRowsList.Select(
            (intermediateResultRow, i) =>
            {
                var newResultRow = i == lastIndex ? resultRow : resultRow.Duplicate();
                newResultRow.Key.Add(new DatabasePageId(parentContext.Alias, intermediateResultRow.Id.RemoveDashes()));

                if (i != lastIndex)
                    resultSet.AddRow(newResultRow);

                return (newResultRow, intermediateResultRow);
            });

        var nextParentContext = parentContext.ParentContext;
        foreach (var (newResultRow, intermediateResultRow) in denormalizedSet)
        {
            if (mapping is not null)
                foreach (var propertyName in mapping.AllSelected
                             ? intermediateResultRow.PropertyNames
                             : mapping.PropertyNames)
                    newResultRow[new FieldIdentifier(parentContext.Alias, propertyName)] =
                        intermediateResultRow[propertyName];


            AddParentRows(resultSet, newResultRow, nextParentContext, intermediateResultRow.ParentRows);
        }
    }

    public override string ToString()
    {
        return "Create result rows";
    }
}