using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestUtil.Request;
using RestUtil.Response;
using HttpMethod = System.Net.Http.HttpMethod;

namespace RestUtil
{
    public class RestClient : IRestClient
    {
        private readonly RestClientOptions _options;
        private readonly ILogger<RestClient> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly RestSharp.IRestClient _implementation = new RestSharp.RestClient();

        private readonly IDictionary<string, string> _defaultHeaders = new Dictionary<string, string>();

        public RestClient(
            IOptions<RestClientOptions> options,
            ILogger<RestClient> logger,
            IServiceProvider serviceProvider)
        {
            _options = options.Value;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public Uri BaseUri
        {
            get => _implementation.BaseUrl;
            set => _implementation.BaseUrl = value;
        }

        public string Token
        {
            set => _implementation.Authenticator = new JwtAuthenticator(value);
        }

        public void AddDefaultHeader(string name, string value)
        {
            _defaultHeaders[name] = value;
        }

        public async Task<IResponse<TResult>> ExecuteAsync<TResult>(IRequest request)
        {
            var restRequest = new RestRequest(request.Path);

            foreach (var (headerName, value) in _defaultHeaders)
            {
                restRequest.AddHeader(headerName, value);
            }

            restRequest.Method = ToRestSharpMethod(request.Method);

            if (request.Body != null)
                restRequest.AddJsonBody(request.Body);

            _logger.LogDebug($"Execute {restRequest.Method} request on {_implementation.BuildUri(restRequest)}", restRequest);
            var response = await _implementation.ExecuteAsync(restRequest);

            if (!response.IsSuccessful)
            {
                _logger.LogError($"Request not successful. Status code: {response.StatusCode}. Response: {response.Content}");
                return new Response<TResult>(response.StatusCode);
            }

            _logger.LogDebug("Request successful.");
            var jsonData = response.Content;

            StoreJsonData(jsonData);

            var settings = new JsonSerializerSettings
            {
                Converters = _serviceProvider.GetServices<JsonConverter>().ToList(),
                NullValueHandling = NullValueHandling.Ignore
            };

            var result = JsonConvert.DeserializeObject<TResult>(jsonData, settings);
            return new Response<TResult>(response.StatusCode, result);
        }

        private void StoreJsonData(string jsonData)
        {
            if (_options.StoreJsonResponse == null)
                return;

            var fileName = Path.Combine(_options.StoreJsonResponse, Path.GetFileName(Path.GetTempFileName()));
            try
            {
                if (!Directory.Exists(_options.StoreJsonResponse))
                    Directory.CreateDirectory(_options.StoreJsonResponse);

                File.WriteAllText(fileName, jsonData);
                _logger.LogDebug($"Wrote json data for request to: {fileName}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Could not write json data to: {fileName}. Error: {ex.Message}");
            }
        }

        private static Method ToRestSharpMethod(HttpMethod requestMethod)
        {
            if (requestMethod == HttpMethod.Post)
                return Method.POST;

            return Method.GET;
        }
    }
}