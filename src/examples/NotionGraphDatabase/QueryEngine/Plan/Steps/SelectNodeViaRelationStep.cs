using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.Storage;
using NotionGraphDatabase.Storage.DataModel;
using NotionGraphDatabase.Storage.Filtering;
using NotionGraphDatabase.Util;
using Util.Extensions;
using Database = NotionGraphDatabase.Metadata.Database;

namespace NotionGraphDatabase.QueryEngine.Plan.Steps;

internal class SelectNodeViaRelationStep : SelectStep
{
    private readonly string _role;

    public SelectNodeViaRelationStep(string role, Database targetDatabase, string targetAlias,
        Filter filter) : base(targetDatabase, targetAlias, filter)
    {
        _role = role;
    }

    public override void Execute(QueryExecutionContext executionContext, IStorageBackend storageBackend)
    {
        var relation = executionContext.Metamodel.Edges.FirstOrDefault(e =>
                (e.From.Alias == _database.Alias && e.Navigability.Reverse.Role == _role)
                || (e.To.Alias == _database.Alias && e.Navigability.Forward.Role == _role))
            .ThrowIfNull(
                $"Relation for role: '{_role}' not found for alias: '{_alias}' (database: {_database.Alias}).");

        var propertyName = relation.From.Alias == _alias
            ? relation.Navigability.Reverse.Label
            : relation.Navigability.Forward.Label;

        var previousResultContext = executionContext.GetCurrentResultContext();

        if (previousResultContext is null)
            throw new Exception(
                $"Cannot select over relation without previous result-context. Current alias: '{_alias}'.");

        var propertyDefinition = previousResultContext.PropertyDefinitions
            .FirstOrDefault(d => d.Name == propertyName)
            .ThrowIfNull(
                $"Property: '{propertyName}' for relational select not found on: '{previousResultContext.Alias}'");

        var database = storageBackend.GetDatabase(_database.Id).ThrowIfNull();
        var nextResultContext = executionContext.GetNextResultContext(database.Definition.Properties, _alias);
        _resolver.SetContext(nextResultContext);

        nextResultContext.AddRange(
            database.Pages
                .Select(p => Join(p, previousResultContext, propertyDefinition))
                .Where(r => r is not null && _filterEngine.Matches(r))!);
    }

    private static IntermediateResultRow? Join(
        DatabasePage page,
        IntermediateResultContext previousResultContext,
        PropertyDefinition propertyDefinition)
    {
        var id = page.Id.RemoveDashes();
        var parentRecords = previousResultContext.IntermediateResultRows.Where(
            r =>
            {
                var propertyValue = r[propertyDefinition.Name];

                if (propertyValue is List<string> list)
                    return list.Any(v => v == id);

                return propertyValue is string strValue && strValue == id;
            }).ToList();

        return !parentRecords.Any() ? null : new IntermediateResultRow(page, parentRecords);
    }

    public override string ToString()
    {
        return $"{base.ToString()} via role [{_role}]";
    }
}