using Microsoft.Extensions.Options;
using NotionGraphDatabase.Interface;
using NotionGraphDatabase.Metadata;

namespace NotionGraphApi;

public class NotionGraphDatabaseModelStore : IMetamodelStore
{
    public Metamodel Metamodel { get; }

    public NotionGraphDatabaseModelStore(IOptions<NotionGraphApiOptions> notionGraphApiOptionsOption)
    {
        var options = notionGraphApiOptionsOption.Value;

        if (options is null)
            throw new Exception($"Missing configuration for {nameof(NotionGraphApi)}");

        Metamodel = options.Metamodel ??
                    throw new Exception($"Missing Metamodel section in {nameof(NotionGraphApi)} configuration");
    }
}