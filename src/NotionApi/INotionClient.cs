using System.Threading.Tasks;

namespace NotionApi
{
    public interface INotionClient
    {
        Task<INotionResponse<TResult>> Execute<TResult>(INotionRequest<TResult> request);
    }
}