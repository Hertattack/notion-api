using Microsoft.Extensions.Logging;

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
    }
}