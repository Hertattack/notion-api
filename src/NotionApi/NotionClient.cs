using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NotionApi.Cache;
using NotionApi.Rest.Response;
using RestUtil;
using RestUtil.Request;
using Util;
using Util.Visitor;

namespace NotionApi;

public class NotionClient : INotionClient
{
    private readonly NotionClientOptions _notionClientOptions;
    private readonly ILogger<NotionClient> _logger;
    private readonly IRestClient _restClient;
    private readonly IRequestBuilder _requestBuilder;
    private readonly IServiceProvider _serviceProvider;

    private long _requestNumber;

    public NotionClient(
        IOptions<NotionClientOptions> options,
        ILogger<NotionClient> logger,
        IRestClient restClient,
        IRequestBuilder requestBuilder,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _restClient = restClient;
        _requestBuilder = requestBuilder;
        _serviceProvider = serviceProvider;

        _notionClientOptions = options.Value;

        _logger.LogInformation($"Using base uri: {_notionClientOptions.BaseUri}", _notionClientOptions.BaseUri);

        _restClient.BaseUri = new Uri(_notionClientOptions.BaseUri);
        _restClient.Token = _notionClientOptions.Token;
        _restClient.AddDefaultHeader("Notion-Version", _notionClientOptions.ApiVersion);
    }

    public async Task<Option<IPaginatedResponse<TResult>>> ExecuteRequest<TResult>(
        IPaginatedNotionRequest<PaginatedResponse<TResult>> notionRequest)
    {
        var myRequest = Interlocked.Add(ref _requestNumber, 1);
        _logger.LogInformation("Performing paginated request {RequestNumber}", myRequest);

        var pageNumber = 0;

        IPaginatedResponse<TResult> result = null;
        while (true)
        {
            pageNumber++;
            if (_notionClientOptions.LimitPagesToRetrieve > 0 && _notionClientOptions.LimitPagesToRetrieve < pageNumber)
                break;

            _logger.LogDebug("Requesting page {PageNumber} for request {RequestNumber}", pageNumber, myRequest);
            var nextResult = await ExecuteRequest<PaginatedResponse<TResult>>(notionRequest);
            _logger.LogDebug("Received response for page {PageNumber} for request {RequestNumber}", pageNumber,
                myRequest);

            if (!nextResult.HasValue)
                break;

            var nextResultValue = nextResult.Value;

            if (result == null)
                result = nextResultValue;
            else
                foreach (var additionalResult in nextResultValue.Results)
                    result.Results.Add(additionalResult);

            if (!nextResultValue.HasMore)
                break;

            notionRequest.SetStartCursor(nextResultValue.NextCursor);
        }

        _logger.LogInformation("Finished paginated request {RequestNumber}", myRequest);
        return Option<IPaginatedResponse<TResult>>.From(result);
    }

    public INotionCache CreateCache()
    {
        var objectVisitorFactory = (IObjectVisitorFactory) _serviceProvider.GetService(typeof(IObjectVisitorFactory));
        var loggerFactory = (ILoggerFactory) _serviceProvider.GetService(typeof(ILoggerFactory));
        return new NotionCache(objectVisitorFactory, loggerFactory);
    }

    public async Task<Option<TResult>> ExecuteRequest<TResult>(INotionRequest<TResult> notionRequest)
    {
        var myRequest = Interlocked.Add(ref _requestNumber, 1);
        var request = _requestBuilder.BuildRequest(notionRequest);

        _logger.LogInformation("Performing request {RequestNumber}", myRequest);
        var result = await _restClient.ExecuteAsync<TResult>(request);
        _logger.LogInformation("Finished request {RequestNumber}", myRequest);

        if (!result.Value.HasValue)
            return Option.None;

        return result.Value.Value;
    }
}