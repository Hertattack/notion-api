using RestUtil.Request;

namespace NotionApi
{
    public interface INotionClient
    {
        TRequestType CreateRequest<TRequestType>() where TRequestType : IRequest;
    }
}