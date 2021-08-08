namespace NotionApi.Cache
{
    public class PropertyConfigurationCacheMiss : ICacheMiss
    {
        private readonly bool _dbInCache;
        public string DatabaseId { get; }
        public string PropertyId { get; }

        public PropertyConfigurationCacheMiss(string databaseId, string propertyId, bool dbInCache)
        {
            _dbInCache = dbInCache;
            DatabaseId = databaseId;
            PropertyId = propertyId;
        }

        public string Description =>
            _dbInCache
                ? $"Property configuration with id: {PropertyId} for database with id: {DatabaseId} could not be found in the cache."
                : $"Property configuration with id: {PropertyId} for database with id: {DatabaseId} could not be found in the cache because the database is not found in the cache.";
    }
}