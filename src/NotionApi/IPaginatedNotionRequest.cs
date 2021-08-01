using NotionApi.Rest;

namespace NotionApi
{
    public interface IPaginatedNotionRequest<TResponse> : INotionRequest<TResponse>, IPaginatedRequest
    {
    }
}