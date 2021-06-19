using System;

namespace RestUtil
{
    public interface IRestClient
    {
        Uri BaseUri { get; set; }
        string Token { set; }
        void Get();
        void AddDefaultHeader(string name, string value);
    }
}