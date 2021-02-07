using System.Threading.Tasks;
using Funda.Infrastructure.Http.Models;

namespace Funda.Infrastructure.Http
{
    public interface IFundaApi
    {
        Task<Object[]> GetObjectsAsync(int pageNumber, int pageSize = 25, bool ObjectWithTuin = false);
    }
}
