using Microsoft.Extensions.Options;
using NotionGraphDatabase.Interface;
using NotionGraphDatabase.Metadata;

namespace NotionGraphApi;

public class NotionGraphDatabaseModelFactory : IMetamodelFactory
{
    private readonly Model _model;

    public NotionGraphDatabaseModelFactory(IOptions<NotionGraphApiOptions> notionGraphApiOptionsOption)
    {
        var options = notionGraphApiOptionsOption.Value;
        if (options is null)
            throw new Exception($"Missing configuration for {nameof(NotionGraphApi)}");

        _model = options.Model;
    }

    public Model CreateModel()
    {
        return _model;
    }
}