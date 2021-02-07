//using EFCore.BulkExtensions;
using EFCore.BulkExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Funda.Data;
using Funda.Synchronizer.Commands;

namespace Funda.Synchronizer.DataPersistence
{
    public class ObjectMassCreateHandler : AsyncRequestHandler<ObjectMassCreateCommand>
    {
        private readonly FundaDbContext _dbContext;
        private readonly ILogger<ObjectMassCreateHandler> _logger;

        public ObjectMassCreateHandler(FundaDbContext dbContext, ILogger<ObjectMassCreateHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;

            // this Migration step should not be here , it should happen during the CI pipline running throw console app that run as a step in the CI tasks.
            //I put it here to just build the database without send a script and make you run it , again this is not the correct place for it 
            _dbContext.Database.Migrate();
        }

        protected override async Task Handle(ObjectMassCreateCommand request, CancellationToken cancellationToken)
        {
            var bulkConfig = new BulkConfig { PreserveInsertOrder = true, SetOutputIdentity = true };

            var objects = new List<Data.Entities.Object>();
            foreach (var obj in request.Objects)
            {
                var show = new Data.Entities.Object
                {
                    Id = obj.Id,
                    Adres = obj.Adres,
                    BronCode = obj.BronCode,
                    Foto = obj.Foto,
                    HasTuin = obj.HasTuin,
                    Huurprijs = obj.Huurprijs,
                    MakelaarId = obj.MakelaarId,
                    MakelaarNaam = obj.MakelaarNaam,
                    MigrationVersion = obj.MigrationVersion,
                    ProjectNaam = obj.ProjectNaam
                };

                objects.Add(show);
            }

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    await _dbContext.BulkInsertOrUpdateAsync(objects, bulkConfig);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"an error occured during save the objects to the DB ", ex);
                    transaction.Rollback();
                }
            }

        }
    }

}
