using System;
using System.Threading.Tasks;
using RestUtil;
using RestUtil.Request;
using RestUtil.Response;

namespace NotionGraphDatabase.Integration.Tests.Util;

public class ProxyRestClient : IRestClient
{
    [field: ThreadStatic] public static ProxyRestClient? LastCreated { get; private set; }

    public IRequest? LastRequest { get; set; }

    public RestClient Client { get; }

    public ProxyRestClient(RestClient client)
    {
        LastCreated = this;
        Client = client;
    }

    public Uri BaseUri
    {
        get => Client.BaseUri;
        set => Client.BaseUri = value;
    }

    public string Token
    {
        set => Client.Token = value;
    }

    public Func<IRequest, bool>? ExecuteRequests { get; set; }

    public void AddDefaultHeader(string name, string value)
    {
        Client.AddDefaultHeader(name, value);
    }

    public Task<IResponse<TResult>> ExecuteAsync<TResult>(IRequest request)
    {
        LastRequest = request;

        if (ExecuteRequests is null || ExecuteRequests.Invoke(request))
            return Client.ExecuteAsync<TResult>(request);

        throw new ProxyException(nameof(ExecuteAsync));
    }

    public TResult DeserializeJson<TResult>(string jsonData)
    {
        return Client.DeserializeJson<TResult>(jsonData);
    }
}