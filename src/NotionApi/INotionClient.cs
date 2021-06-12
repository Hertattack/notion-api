using NotionApi.Request;
using NotionApi.Rest;

namespace NotionApi
{
    public interface INotionClient
    {
        TRequestType CreateRequest<TRequestType>() where TRequestType : IRequest;
    }
}