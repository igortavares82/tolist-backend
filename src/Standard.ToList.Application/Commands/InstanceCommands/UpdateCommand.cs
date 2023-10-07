using MediatR;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application.Commands.InstanceCommands
{
    public class UpdateCommand : Request, IRequest<Result<Unit>>
	{
		public string InstanceId { get; set; }
		public bool IsEnabled { get; set; }
		public string Name { get; set; }
	}
}

