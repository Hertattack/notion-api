using NotionGraphDatabase.Util;

namespace NotionGraphDatabase.Metadata;

public class Database
{
    private string? _id = null;

    public string? Id
    {
        get => _id;
        set => _id = value?.AddDashes();
    }

    public string Alias { get; set; } = null!;
}