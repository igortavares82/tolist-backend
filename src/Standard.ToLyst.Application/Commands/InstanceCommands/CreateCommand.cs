using MediatR;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.ViewModels.Lysts;

namespace Standard.ToLyst.Application.Commands.InstanceCommands
{
    public class CreateCommand : Request, IRequest<Result<InstanceViewModel>>
	{
		public string Name { get; set; }
	}
}
