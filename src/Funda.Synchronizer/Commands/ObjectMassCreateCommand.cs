using MediatR;
using System.Collections.Generic;
using Funda.Synchronizer.DomainModels;

namespace Funda.Synchronizer.Commands
{
    public class ObjectMassCreateCommand : IRequest
    {
        public List<Object> Objects { get; set; }

    }
}
