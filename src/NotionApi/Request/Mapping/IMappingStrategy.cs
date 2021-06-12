namespace NotionApi.Request.Mapping
{
    public interface IMappingStrategy
    {
        object GetValue(object valueToMap);
    }
}