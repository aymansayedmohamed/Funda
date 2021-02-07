using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Funda.Data;
using Funda.Queries;
using Funda.Responses;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Funda.Handlers
{
    public class TopMakelaarObjectsHandler : IRequestHandler<TopMakelaarObjectsQuery, MakelaarObjectsResponseModel>
    {
        private readonly FundaDbContext _dbContext;

        public TopMakelaarObjectsHandler(FundaDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<MakelaarObjectsResponseModel> Handle(TopMakelaarObjectsQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var objects = _dbContext.Objects.Where(o => o.HasTuin == request.ObjectHasTuin)
                                            .GroupBy(o => o.MakelaarId)
                                            .OrderByDescending(g => g.Count()).AsNoTracking()
                                            .Skip(request.pageSize * (request.pageNumber - 1))
                                            .Take(request.pageSize);

            MakelaarObjectsResponseModel respone = new MakelaarObjectsResponseModel();

            respone.MakelaarObjects = objects.Select(g => new Models.MakelaarObject()
            {
                Makelaar = new Models.Makelaar()
                {
                    Id = g.FirstOrDefault().MakelaarId,
                    Naam = g.FirstOrDefault().MakelaarNaam
                },
                Objects = g.Select(o => new Models.Object()
                {
                    Id = o.Id,
                    Adres = o.Adres,
                    BronCode = o.BronCode,
                    Foto = o.Foto,
                    hasTuin = o.HasTuin,
                    Huurprijs = o.Huurprijs,
                    ProjectNaam = o.MakelaarNaam,
                }).ToArray()
            }).ToArray();

            return respone;

        }
    }
}
