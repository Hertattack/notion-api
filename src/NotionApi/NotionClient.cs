using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestUtil;
using RestUtil.Request;

namespace NotionApi
{
    public class NotionClient : INotionClient
    {
        private readonly NotionClientOptions _notionClientOptions;
        private readonly ILogger<NotionClient> _logger;
        private readonly ITokenProvider _tokenProvider;
        private readonly IRestClient _restClient;
        private readonly IRequestBuilder _requestBuilder;

        public NotionClient(
            IOptions<NotionClientOptions> options,
            ILogger<NotionClient> logger,
            ITokenProvider tokenProvider,
            IRestClient restClient,
            IRequestBuilder requestBuilder)
        {
            _logger = logger;
            _tokenProvider = tokenProvider;
            _restClient = restClient;
            _requestBuilder = requestBuilder;

            _notionClientOptions = options.Value;

            _logger.LogInformation($"Using base uri: {_notionClientOptions.BaseUri}", _notionClientOptions.BaseUri);

            _restClient.BaseUri = new Uri(new Uri(_notionClientOptions.BaseUri), "/v1");
            _restClient.Token = _notionClientOptions.Token;

            _restClient.AddDefaultHeader("Notion-Version", _notionClientOptions.ApiVersion);
            _restClient.Get();
        }

        public TRequestType CreateRequest<TRequestType>() where TRequestType : IRequest
        {
            var requestType = typeof(TRequestType);
            var constructor = requestType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(IsSupportedConstructor);

            if (constructor == null)
                throw new ArgumentException($"The request type: {requestType.FullName} does not have a supported constructor.");

            return (TRequestType) constructor.Invoke(new object[] {this, _requestBuilder});
        }

        private static bool IsSupportedConstructor(ConstructorInfo info)
        {
            var parameters = info.GetParameters();
            return parameters.Length == 2
                   && parameters[0].ParameterType == typeof(INotionClient)
                   && parameters[1].ParameterType == typeof(IRequestBuilder);
        }
    }
}