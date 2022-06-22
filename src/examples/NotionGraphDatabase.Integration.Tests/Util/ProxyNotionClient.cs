using System;
using System.Threading.Tasks;
using NotionApi;
using NotionApi.Cache;
using NotionApi.Rest.Response;
using Util;

namespace NotionGraphDatabase.Integration.Tests.Util;

public class ProxyNotionClient : INotionClient
{
    [field: ThreadStatic] public static ProxyNotionClient? LastCreated { get; private set; }

    public NotionClient Client { get; }

    public ProxyNotionClient(NotionClient client)
    {
        LastCreated = this;
        Client = client;
    }

    public Task<Option<IPaginatedResponse<TResult>>> ExecuteRequest<TResult>(
        IPaginatedNotionRequest<PaginatedResponse<TResult>> notionRequest)
    {
        return Client.ExecuteRequest(notionRequest);
    }

    public Task<Option<TResult>> ExecuteRequest<TResult>(INotionRequest<TResult> notionRequest)
    {
        return Client.ExecuteRequest(notionRequest);
    }

    public INotionCache CreateCache()
    {
        return Client.CreateCache();
    }
}