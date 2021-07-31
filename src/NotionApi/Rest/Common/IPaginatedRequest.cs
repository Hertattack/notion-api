namespace NotionApi.Rest.Common
{
    public interface IPaginatedRequest
    {
        void SetStartCursor(string value);
    }
}