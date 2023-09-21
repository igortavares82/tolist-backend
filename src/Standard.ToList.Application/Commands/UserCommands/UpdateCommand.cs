using MediatR;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application.Commands.UserCommands
{
    public class UpdateCommand : Request, IRequest<Result<Unit>>
	{
		public string Name { get; set; }
		public string Password { get; set; }
    }
}

