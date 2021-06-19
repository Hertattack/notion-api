using System.Net.Http;

namespace RestUtil.Request
{
    public class RequestBuilder : IRequestBuilder
    {
        private readonly IMapper _mapper;

        public RequestBuilder(IMapper mapper)
        {
            _mapper = mapper;
        }
        
        public IRequest BuildRequest(HttpMethod method, object search)
        {
            var parameters = _mapper.Map(search);

            return null;
        }
    }
}