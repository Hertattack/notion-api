namespace NotionApi.Rest
{
    public interface IPaginatedRequest
    {
        void SetStartCursor(string value);
    }
}