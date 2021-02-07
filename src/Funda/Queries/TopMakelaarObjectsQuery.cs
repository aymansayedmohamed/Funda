using MediatR;
using Funda.Responses;

namespace Funda.Queries
{
    public class TopMakelaarObjectsQuery : IRequest<MakelaarObjectsResponseModel>
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public bool ObjectHasTuin { get; set; }

    }
}
