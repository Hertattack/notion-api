using System;
using Microsoft.Extensions.Logging;
using NotionApi.Request;
using NotionApi.Rest;

namespace NotionApi
{
    internal class NotionClient : INotionClient
    {
        public static string BASE_URL = "https://api.notion.com";

        private readonly ILogger<NotionClient> _logger;
        private readonly ITokenProvider _tokenProvider;

        public NotionClient(
            ILogger<NotionClient> logger,
            ITokenProvider tokenProvider)
        {
            _logger = logger;
            _tokenProvider = tokenProvider;
        }

        public TRequestType CreateRequest<TRequestType>() where TRequestType : IRequest
        {
            return (TRequestType) Activator.CreateInstance(typeof(TRequestType), this);
        }
    }
}