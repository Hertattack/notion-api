using NotionApi.Rest.Common;

namespace NotionApi
{
    public interface IPaginatedNotionRequest<TResponse> : INotionRequest<TResponse>, IPaginatedRequest
    {
    }
}