using MediatR;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Application.Commands.InstanceCommands
{
    public class UpdateCommand : Request, IRequest<Result<Unit>>
	{
		public string InstanceId { get; set; }
		public bool? IsEnabled { get; set; }
		public string Name { get; set; }
	}
}
