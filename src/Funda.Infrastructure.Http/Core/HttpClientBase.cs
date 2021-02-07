using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Funda.Infrastructure.Http.Exceptions;

namespace Funda.Infrastructure.Http.Core
{
    public abstract class HttpClientBase
    {
        protected readonly HttpClient _httpClient;

        protected readonly ILogger<HttpClientBase> _logger;

        public HttpClientBase(HttpClient httpClient, ILogger<HttpClientBase> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public virtual Task<HttpClientBaseResponse<TResponse>> Get<TResponse>(string requestUri)
        {
            try
            {
                _logger.LogDebug($"Invoking a GET request to {_httpClient.BaseAddress}/{requestUri}.");

                return ProcessRequest<TResponse>(() =>
                    _httpClient.GetAsync(
                        requestUri));
            }
            catch(Exception ex)
            {
                throw;
            }
        }


        public virtual async Task<HttpClientBaseResponse<TResponse>> ProcessRequest<TResponse>(Func<Task<HttpResponseMessage>> call)
        {
            using (var response = await call())
            {
                var raw = await response.Content.ReadAsStringAsync();

                _logger.LogDebug($"Invoked a request to {response.RequestMessage.RequestUri} | Status: {response.StatusCode}.");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpFailedRequestException(response.ReasonPhrase, response.StatusCode);
                }

                return new HttpClientBaseResponse<TResponse>(raw);
            }
        }

    }
}
