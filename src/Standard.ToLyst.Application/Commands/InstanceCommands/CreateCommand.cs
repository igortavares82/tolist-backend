using MediatR;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.ViewModels.Lysts;

namespace Standard.ToList.Application.Commands.InstanceCommands
{
    public class CreateCommand : Request, IRequest<Result<InstanceViewModel>>
	{
		public string Name { get; set; }
	}
}
