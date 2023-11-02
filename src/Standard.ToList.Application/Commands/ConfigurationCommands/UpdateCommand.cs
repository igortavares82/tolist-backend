using System;
using MediatR;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application.Commands.ConfigurationCommands
{
    public class UpdateCommand : Request, IRequest<Result<Unit>>
    {
        public string Name { get; set; }
        public WorkerCommand[] Workers { get; set; }
    }
}
