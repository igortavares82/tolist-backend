using MediatR;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.ViewModels.Configuration;

namespace Standard.ToList.Application.Commands.ConfigurationCommands
{
    public class CreateCommand : Request, IRequest<Result<ConfigurationViewModel>>
	{
        public string Name { get; set; }
        public WorkerCommand[] Workers { get; set; }
    }
}
