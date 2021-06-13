using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using NotionApi.Request;

namespace NotionApi
{
    internal class NotionClient : INotionClient
    {
        public static string BASE_URL = "https://api.notion.com";

        private readonly ILogger<NotionClient> _logger;
        private readonly ITokenProvider _tokenProvider;
        private readonly IRequestBuilder _requestBuilder;

        public NotionClient(
            ILogger<NotionClient> logger,
            ITokenProvider tokenProvider,
            IRequestBuilder requestBuilder)
        {
            _logger = logger;
            _tokenProvider = tokenProvider;
            _requestBuilder = requestBuilder;
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