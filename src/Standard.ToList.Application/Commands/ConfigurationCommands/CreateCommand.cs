using MediatR;
using Standard.ToList.Model.Aggregates.Configuration;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.ViewModels.Configuration;

namespace Standard.ToList.Application.Commands.ConfigurationCommands
{
    public class CreateCommand : Request, IRequest<Result<ConfigurationViewModel>>
	{
        public string Name { get; set; }
        public WorkerCommand[] Workers { get; set; }

    }

    public class WorkerCommand
    {
        public WorkerType Type { get; set; }
        public int Delay { get; set; }
        public int Items { get; set; } = 100;
        public int Next { get; set; } = 1;
        public string Properties { get; set; }
    }
}

