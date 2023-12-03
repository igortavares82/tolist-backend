using MediatR;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Application.Commands.UserCommands
{
    public class UpdateCommand : Request, IRequest<Result<Unit>>
	{
		public string Name { get; set; }
		public string Password { get; set; }
    }
}
