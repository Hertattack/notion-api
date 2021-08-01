using System.Threading.Tasks;
using NotionApi.Rest;
using Util;

namespace NotionApi
{
    public interface INotionClient
    {
        Task<Option<TResult>> ExecuteRequest<TResult>(INotionRequest<TResult> request);

        Task<Option<IPaginatedResponse<TResult>>> ExecuteRequest<TResult>(
            IPaginatedNotionRequest<PaginatedResponse<TResult>> notionRequest);
    }
}