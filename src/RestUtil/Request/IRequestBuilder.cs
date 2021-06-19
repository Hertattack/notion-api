using System.Net.Http;

namespace RestUtil.Request
{
    public interface IRequestBuilder
    {
        IRequest BuildRequest(HttpMethod post, object search);
    }
}