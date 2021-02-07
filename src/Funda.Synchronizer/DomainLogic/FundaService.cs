using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Funda.Infrastructure.Http;
using MediatR;
using System;
using Funda.Synchronizer.Commands;
using System.Collections.Generic;
using SyncDomainObject = Funda.Synchronizer.DomainModels.Object;
using FundaApiObject = Funda.Infrastructure.Http.Models.Object;

namespace Funda.Synchronizer.DomainLogic
{
    public class FundaService : IFundaService
    {
        private readonly IFundaApi _fundaApi;
        private readonly ILogger<FundaService> _logger;
        private readonly IMediator _mediator;

        private const int pageSize = 25;

        public FundaService(IFundaApi fundaApi, ILogger<FundaService> logger, IMediator mediator)
        {
            _fundaApi = fundaApi;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task PersistFundaApiObjectsToDb(ILogger log = null)
        {
            var migrationVersion = Guid.NewGuid().ToString("N");

            await PersistObjectsToDb(false, migrationVersion, log);

            await PersistObjectsToDb(true, migrationVersion, log);

            await CleanDbAfterMigration(migrationVersion);
        }

        private async Task PersistObjectsToDb(bool withTuin, string migrationVersion, ILogger log = null)
        {
            int currentPage = 0;
            FundaApiObject[] objects;
            try
            {
                while ((objects = await GetObjectsAsync(currentPage, withTuin)).Any())
                {
                    await SavePage(objects, withTuin, migrationVersion);

                    currentPage++;

                    var message = $": {currentPage} pages had been synchronized for objects {(withTuin ? "with" : "without")} tuin";

                    if (log is null)
                        _logger.LogInformation(message);
                    else
                        log.LogInformation(message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"an error occured during retrieve objects {(withTuin ? "with" : "without")} tuin and persist it for page number: {currentPage} ", ex);
            }
        }

        private async Task SavePage(FundaApiObject[] objects, bool withTuin, string migrationVersion)
        {
            var tvShowMassCreateCommand = new ObjectMassCreateCommand() { Objects = new List<SyncDomainObject>() };

            foreach (var obj in objects)
            {
                var row = new SyncDomainObject
                {
                    Id = obj.Id,
                    Adres = obj.Adres,
                    ProjectNaam = obj.ProjectNaam,
                    BronCode = obj.BronCode,
                    Foto = obj.Foto,
                    HasTuin = withTuin,
                    Huurprijs = obj.Huurprijs,
                    MakelaarId = obj.MakelaarId,
                    MakelaarNaam = obj.MakelaarNaam,
                    MigrationVersion = migrationVersion,
                };

                tvShowMassCreateCommand.Objects.Add(row);
            }

            await _mediator.Send(tvShowMassCreateCommand);
        }

        private async Task CleanDbAfterMigration(string migrationVersion)
        {
            try
            {
                var cleanObjectsMigrationCommand = new CleanObjectsMigrationCommand() { MigrationVersion = migrationVersion };

                await _mediator.Send(cleanObjectsMigrationCommand);
            }
            catch (Exception ex)
            {
                _logger.LogError($"an error occured during clean the database after the last migration", ex);
            }
        }

        private async Task<FundaApiObject[]> GetObjectsAsync(int pageNumber, bool withTuin)
        {
            try
            {
                var objects = await _fundaApi.GetObjectsAsync(pageNumber, pageSize, withTuin);
                return objects.Select(o => new FundaApiObject()
                {
                    MakelaarId = o.MakelaarId,
                    Id = o.Id,
                    MakelaarNaam = o.MakelaarNaam,
                    Adres = o.Adres,
                    BronCode = o.BronCode,
                    Foto = o.Foto,
                    Huurprijs = o.Huurprijs,
                    ProjectNaam = o.ProjectNaam,
                }).ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError("an error ocurred during call Funda api ", ex);
                throw;
            }
        }
    }
}
