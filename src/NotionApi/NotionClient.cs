using System.Net.Http;
using FluentRest;
using FluentRest.Extensions;
using Microsoft.Extensions.Logging;
using NotionApi.Structure;

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

            var builder = new FluentRestApiBuilder(Version.V20210513.ToString());

            builder.WithBase("/v1")
                .AddPath("search")
                .AddOperation(HttpMethod.Post)
                .AddBodySpecification<ISearchBody>()
                .Sort.IsRequired();
        }

        public Version ApiVersion { get; set; }
    }
}