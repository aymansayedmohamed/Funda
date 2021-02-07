using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Funda.Queries;
using Microsoft.Extensions.Logging;

namespace Funda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<FundaController> _logger;


        public FundaController(IMediator mediator, ILogger<FundaController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<Models.MakelaarObject[]>> Get(int pageNumber = 1, int pageSize = 10, bool tuin = false)

        {
            try
            {
                var topMakeelerObjectsQuery = new TopMakelaarObjectsQuery() { pageNumber = pageNumber, pageSize = pageSize, ObjectHasTuin = tuin };

                var result = await _mediator.Send(topMakeelerObjectsQuery);
                return result.MakelaarObjects;
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"an error occured during retrieve Makelaars Objects ", ex);
                return null;
            }
        }

    }
}
