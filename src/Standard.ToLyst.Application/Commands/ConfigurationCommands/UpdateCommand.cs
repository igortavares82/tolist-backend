using System;
using MediatR;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Application.Commands.ConfigurationCommands
{
    public class UpdateCommand : Request, IRequest<Result<Unit>>
    {
        public string Name { get; set; }
        public WorkerCommand[] Workers { get; set; }
        public LoggerCommand Logger { get; set; }
    }
}
