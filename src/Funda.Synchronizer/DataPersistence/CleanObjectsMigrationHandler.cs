using EFCore.BulkExtensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Funda.Data;
using Funda.Synchronizer.Commands;
using System.Linq;

namespace Funda.Synchronizer.DataPersistence
{
    public class CleanObjectsMigrationHandler : AsyncRequestHandler<CleanObjectsMigrationCommand>
    {
        private readonly FundaDbContext _dbContext;
        private readonly ILogger<CleanObjectsMigrationHandler> _logger;

        public CleanObjectsMigrationHandler(FundaDbContext dbContext, ILogger<CleanObjectsMigrationHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        protected override async Task Handle(CleanObjectsMigrationCommand request, CancellationToken cancellationToken)
        {
            var bulkConfig = new BulkConfig { PreserveInsertOrder = true, SetOutputIdentity = true };

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var x = _dbContext.Objects.Where(o => o.MigrationVersion != request.MigrationVersion).ToList();
                    _dbContext.BulkDelete(x);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"an error occurred during clean objects which are not consistent with the latest migration version from the DB", ex);
                    transaction.Rollback();
                }
            }

        }
    }

}
