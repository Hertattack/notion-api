using System.Net;
using Util;

namespace RestUtil.Response
{
    public class Response<TResult> : IResponse<TResult>
    {
        public Option<TResult> Value { get; }
        public HttpStatusCode StatusCode { get; }

        public Response(HttpStatusCode statusCode)
        {
            Value = Option.None;
            StatusCode = statusCode;
        }

        public Response(HttpStatusCode statusCode, TResult result)
        {
            StatusCode = statusCode;
            Value = result;
        }
    }
}