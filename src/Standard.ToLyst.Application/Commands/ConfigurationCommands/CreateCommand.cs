using MediatR;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.ViewModels.Configuration;

namespace Standard.ToLyst.Application.Commands.ConfigurationCommands
{
    public class CreateCommand : Request, IRequest<Result<ConfigurationViewModel>>
	{
        public string Name { get; set; }
        public WorkerCommand[] Workers { get; set; }
        public LoggerCommand Logger { get; set; }
    }
}
