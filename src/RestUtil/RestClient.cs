using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RestSharp;
using RestSharp.Authenticators;
using RestUtil.Request;
using RestUtil.Response;
using HttpMethod = System.Net.Http.HttpMethod;

namespace RestUtil
{
    public class RestClient : IRestClient
    {
        private readonly ILogger<RestClient> _logger;
        private readonly RestSharp.IRestClient _implementation = new RestSharp.RestClient();

        private readonly IDictionary<string, string> _defaultHeaders = new Dictionary<string, string>();

        public RestClient(ILogger<RestClient> logger)
        {
            _logger = logger;
        }

        public Uri BaseUri
        {
            get => _implementation.BaseUrl;
            set => _implementation.BaseUrl = value;
        }

        public string Token
        {
            set => _implementation.Authenticator = new JwtAuthenticator(value);
        }

        public void AddDefaultHeader(string name, string value)
        {
            _defaultHeaders[name] = value;
        }

        public async Task<IResponse> Execute(IRequest request)
        {
            var restRequest = new RestRequest(request.Path);

            foreach (var (headerName, value) in _defaultHeaders)
            {
                restRequest.AddHeader(headerName, value);
            }

            restRequest.Method = ToRestSharpMethod(request.Method);

            if (request.Body != null)
                restRequest.AddJsonBody(request.Body);

            _logger.LogDebug($"Execute {restRequest.Method} request on {_implementation.BuildUri(restRequest)}", restRequest);
            var result = await _implementation.ExecuteAsync(restRequest);

            Console.Write(result.Content);

            return null;
        }

        private static Method ToRestSharpMethod(HttpMethod requestMethod)
        {
            if (requestMethod == HttpMethod.Post)
                return Method.POST;

            return Method.GET;
        }
    }
}