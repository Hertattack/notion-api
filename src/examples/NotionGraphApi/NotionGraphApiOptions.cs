using NotionGraphDatabase.Metadata;

namespace NotionGraphApi;

public class NotionGraphApiOptions
{
    public Metamodel Metamodel { get; set; } = null!;

    public string Culture { get; set; } = null!;
}