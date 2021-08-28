namespace NotionApi.Rest.Request
{
    public interface IPaginatedRequest
    {
        void SetStartCursor(string value);
    }
}