using System.Threading.Tasks;
using NotionApi.Cache;
using NotionApi.Rest;
using NotionApi.Rest.Response;
using Util;

namespace NotionApi
{
    public interface INotionClient
    {
        Task<Option<IPaginatedResponse<TResult>>> ExecuteRequest<TResult>(
            IPaginatedNotionRequest<PaginatedResponse<TResult>> notionRequest);
        
        Task<Option<TResult>> ExecuteRequest<TResult>(
            INotionRequest<TResult> notionRequest);

        INotionCache CreateCache();
    }
}