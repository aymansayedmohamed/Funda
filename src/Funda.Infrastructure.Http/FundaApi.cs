using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using Funda.Infrastructure.Http.Core;
using Funda.Infrastructure.Http.Models;
using Polly;
using System;

namespace Funda.Infrastructure.Http
{
    public class FundaApi : HttpClientBase, IFundaApi
    {
        private readonly ILogger<FundaApi> _logger;

        //The sensitive data should be stored at secure storage like Azure keyvalut
        private const string apiKey = "ac1b0b1572524640a0ecc54de453ea9f";

        public FundaApi(HttpClient httpClient, ILogger<FundaApi> logger)
            : base(httpClient, logger)
        {
            _logger = logger;
        }

        public async Task<Models.Object[]> GetObjectsAsync(int pageNumber, int pageSize = 25, bool ObjectWithTuin = false)
        {
            return await AsyncRetrySyntax.WaitAndRetryAsync(Policy.Handle<System.Exception>(ex => ex is HttpRequestException), 7, // exponential back-off
                                     retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                                     (_, timespan, __, ___) =>
                                             _logger.LogWarning($"{nameof(FundaApi)} calling failed, waiting {timespan.TotalMilliseconds} ms before retrying..."))
                                     .ExecuteAsync(async () =>
                                     {
                                         var response = await Get<FundaApiResponse>($"/feeds/Aanbod.svc/json/{apiKey}/?type=koop&zo=/amsterdam{(ObjectWithTuin ? "/tuin" : string.Empty)}/&page={pageNumber}&pagesize={pageSize}");

                                         return response.Result.Objects.ToArray();

                                     }).ConfigureAwait(false);

        }

    }
}
