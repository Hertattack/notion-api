namespace NotionApi.Cache;

public class ObjectReferenceCacheMiss : ICacheMiss
{
    public string Type { get; }
    public string Id { get; }

    public ObjectReferenceCacheMiss(string type, string id)
    {
        Type = type;
        Id = id;
    }

    public string Description => $"Reference of type: {Type} with id: {Id} could not be found in the cache.";
}