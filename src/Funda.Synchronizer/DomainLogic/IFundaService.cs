using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Funda.Synchronizer.DomainLogic
{
    public interface IFundaService
    {
        Task PersistFundaApiObjectsToDb(ILogger log = null);
    }
}
