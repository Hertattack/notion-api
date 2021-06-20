using System.Threading.Tasks;
using RestUtil.Request;
using RestUtil.Response;

namespace NotionApi
{
    public interface INotionClient
    {
        TRequestType CreateRequest<TRequestType>() where TRequestType : IRequest;
        Task<IResponse> Execute(IRequest request);
    }
}