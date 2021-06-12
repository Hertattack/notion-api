using System.Net.Http;

namespace NotionApi.Request
{
    public interface IRequestBuilder
    {
        IRequest BuildRequest(HttpMethod post, object search);
    }
}