using NotionApi.Rest;
using NotionApi.Rest.Request;

namespace NotionApi;

public interface IPaginatedNotionRequest<TResponse> : INotionRequest<TResponse>, IPaginatedRequest
{
}