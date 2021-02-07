using MediatR;

namespace Funda.Synchronizer.Commands
{
    public class CleanObjectsMigrationCommand : IRequest
    {
        public string MigrationVersion { get; set; }

    }
}
