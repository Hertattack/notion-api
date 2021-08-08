﻿using System;
using System.Threading.Tasks;
using RestUtil.Request;
using RestUtil.Response;

namespace RestUtil
{
    public interface IRestClient
    {
        Uri BaseUri { get; set; }
        string Token { set; }
        void AddDefaultHeader(string name, string value);
        Task<IResponse<TResult>> ExecuteAsync<TResult>(IRequest request);
        TResult DeserializeJson<TResult>(string jsonData);
    }
}