namespace NotionApi.Util
{
    public interface IOption
    {
        bool HasValue { get; }
        object GetValue();
    }
}