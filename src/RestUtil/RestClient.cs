using System;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Authenticators;

namespace RestUtil
{
    public class RestClient : IRestClient
    {
        private readonly RestSharp.IRestClient _implementation = new RestSharp.RestClient();

        private readonly IDictionary<string, string> _defaultHeaders = new Dictionary<string, string>();

        public Uri BaseUri
        {
            get => _implementation.BaseUrl;
            set => _implementation.BaseUrl = value;
        }

        public string Token
        {
            set => _implementation.Authenticator = new JwtAuthenticator(value);
        }

        public void Get()
        {
            var request = new RestRequest("users");

            foreach (var (headerName, value) in _defaultHeaders)
            {
                request.AddHeader(headerName, value);
            }

            Console.Write(_implementation.Get(request).Content);
        }

        public void AddDefaultHeader(string name, string value)
        {
            _defaultHeaders[name] = value;
        }
    }
}