using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.Storage;
using NotionGraphDatabase.Storage.Filtering;
using Util.Extensions;

namespace NotionGraphDatabase.QueryEngine.Plan.Steps;

internal class SelectFromNodeStep : SelectStep
{
    public SelectFromNodeStep(Database database, string alias, Filter? filter) : base(database, alias, filter)
    {
    }

    public override void Execute(QueryExecutionContext executionContext, IStorageBackend storageBackend)
    {
        var previousResultContext = executionContext.GetCurrentResultContext();

        if (previousResultContext is not null)
            throw new Exception("Only one select-step supported.");

        var database = storageBackend.GetDatabase(_database.Id).ThrowIfNull();
        var nextResultContext = executionContext.GetNextResultContext(database.Definition.Properties, _alias);
        _resolver.SetContext(nextResultContext);

        nextResultContext.AddRange(
            database.Pages
                .Select(p => new IntermediateResultRow(p))
                .Where(_filterEngine.Matches)
        );
    }

    public override string ToString()
    {
        return $"Select node '{_database.Id}' ({_alias})";
    }
}