using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Funda.Infrastructure.Http;
using Funda.Data;
using Microsoft.EntityFrameworkCore;
using Funda.Synchronizer.DomainLogic;
using Polly;
using System.Net.Http;
using Polly.Extensions.Http;

[assembly: FunctionsStartup(typeof(Funda.Synchronizer.Startup))]
namespace Funda.Synchronizer
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<FundaDbContext>(cfg =>
            {
                var x = Environment.GetEnvironmentVariable("ConnectionStrings:Funda");
                cfg.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings:Funda"), options => options.MigrationsAssembly("Funda.Synchronizer"));
            });

            builder.Services.AddMediatR(typeof(Startup));
            builder.Services.AddTransient<IFundaService, FundaService>();
            builder.Services.AddHttpClient<IFundaApi, FundaApi>(client =>
            {
                client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("FundaApiUrl"));
            }).AddPolicyHandler(GetRetryPolicy());
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
