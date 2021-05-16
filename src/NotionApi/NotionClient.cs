using Microsoft.Extensions.Logging;
using NotionApi.Commands;
using NotionApi.Commands.Builder;

namespace NotionApi
{
    internal class NotionClient : INotionClient
    {
        public static string BASE_URL = "https://api.notion.com";
        
        private readonly ILogger<NotionClient> _logger;
        private readonly ITokenProvider _tokenProvider;
        private readonly CommandBuilderFactory _commandBuilderFactory;

        public NotionClient(
            ILogger<NotionClient> logger,
            ITokenProvider tokenProvider)
        {
            _logger = logger;
            _tokenProvider = tokenProvider;
            _commandBuilderFactory = new CommandBuilderFactory();
        }

        public Version ApiVersion { get; set; } = Version.V20210513;
        
        public ICommandBuilder<T> GetCommandBuilder<T>() where T : ICommand =>
            _commandBuilderFactory.GetCommandBuilder<T>(ApiVersion);
    }
}