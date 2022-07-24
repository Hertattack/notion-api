namespace NotionGraphDatabase.QueryEngine;

public struct DatabasePageId
{
    public DatabasePageId(string alias, string id)
    {
        Alias = alias;
        Id = id;
    }

    public string Alias { get; set; }
    public string Id { get; set; }
}