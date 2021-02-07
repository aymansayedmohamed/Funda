using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Funda.Synchronizer.DomainLogic;

namespace Funda.Synchronizer
{
    public class FundaTimerEventTrigger
    {
        private readonly IFundaService _fundaService;
        private ILogger _logger;

        public FundaTimerEventTrigger(IFundaService fundaService)
        {
            _fundaService = fundaService;
        }

        // now the function run every minute for the demo purpose. the schedule should depend on the API data update behavior
        [FunctionName("FundaTimerEventTrigger")]
        public async Task RunAsync([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger logger)
        {
            _logger = logger;
            _logger.LogInformation($"C# Funda Timer Event Trigger function executed at: {DateTime.Now}");

            await _fundaService.PersistFundaApiObjectsToDb(logger);
        }


    }
}

