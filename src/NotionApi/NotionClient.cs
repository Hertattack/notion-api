using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NotionApi.Rest;
using RestUtil;
using RestUtil.Request;

namespace NotionApi
{
    public class NotionClient : INotionClient
    {
        private readonly NotionClientOptions _notionClientOptions;
        private readonly ILogger<NotionClient> _logger;
        private readonly IRestClient _restClient;
        private readonly IRequestBuilder _requestBuilder;

        public NotionClient(
            IOptions<NotionClientOptions> options,
            ILogger<NotionClient> logger,
            IRestClient restClient,
            IRequestBuilder requestBuilder)
        {
            _logger = logger;
            _restClient = restClient;
            _requestBuilder = requestBuilder;

            _notionClientOptions = options.Value;

            _logger.LogInformation($"Using base uri: {_notionClientOptions.BaseUri}", _notionClientOptions.BaseUri);

            _restClient.BaseUri = new Uri(_notionClientOptions.BaseUri);
            _restClient.Token = _notionClientOptions.Token;
            _restClient.AddDefaultHeader("Notion-Version", _notionClientOptions.ApiVersion);
        }

        public async Task<INotionResponse<TResult>> Execute<TResult>(INotionRequest<TResult> notionRequest)
        {
            var request = _requestBuilder.BuildRequest(notionRequest);

            var result = await _restClient.ExecuteAsync<TResult>(request);

            if (result.Value.HasValue)
                return new NotionResponse<TResult>(result.Value.Value);
            else
                return new NotionResponse<TResult>(default);
        }
    }
}