using System;

namespace RestUtil.Request.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RequestAttribute : Attribute
    {
        public string Path { get; set; }

        public HttpMethod Method { get; set; }

        public System.Net.Http.HttpMethod HttpMethod =>
            Method == RestUtil.Request.HttpMethod.Post ? System.Net.Http.HttpMethod.Post : System.Net.Http.HttpMethod.Get;
    }
}