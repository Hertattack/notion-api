using System.Threading.Tasks;
using NotionApi.Cache;
using NotionApi.Rest;
using Util;

namespace NotionApi
{
    public interface INotionClient
    {
        Task<Option<IPaginatedResponse<TResult>>> ExecuteRequest<TResult>(
            IPaginatedNotionRequest<PaginatedResponse<TResult>> notionRequest);

        Task<Option<IPaginatedResponse<TResult>>> ReadFromDisk<TResult>(
            IPaginatedNotionRequest<PaginatedResponse<TResult>> notionRequest, string directory);

        INotionCache CreateCache();
    }
}