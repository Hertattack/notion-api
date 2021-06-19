using System;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Authenticators;

namespace RestUtil
{
    public class RestClient : IRestClient
    {
        private readonly RestSharp.IRestClient implementation = new RestSharp.RestClient();

        private readonly IDictionary<string, string> defaultHeaders = new Dictionary<string, string>();

        public Uri BaseUri
        {
            get => implementation.BaseUrl;
            set => implementation.BaseUrl = value;
        }

        public string Token
        {
            set => implementation.Authenticator = new JwtAuthenticator(value);
        }

        public void Get()
        {
            var request = new RestRequest("users");

            foreach (var (headerName, value) in defaultHeaders)
            {
                request.AddHeader(headerName, value);
            }

            Console.Write(implementation.Get(request).Content);
        }

        public void AddDefaultHeader(string name, string value)
        {
            defaultHeaders[name] = value;
        }
    }
}