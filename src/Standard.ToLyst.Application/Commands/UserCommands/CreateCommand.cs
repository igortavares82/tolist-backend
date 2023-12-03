using MediatR;
using Standard.ToLyst.Model.Aggregates.Users;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.ViewModels.Users;

namespace Standard.ToLyst.Application.Commands.UserCommands
{
	public class CreateCommand : IRequest<Result<UserViewModel>>
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public RoleType Role {get; set; }
	}
}

