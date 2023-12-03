using MediatR;
using Standard.ToList.Model.Aggregates.Users;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.ViewModels.Users;

namespace Standard.ToList.Application.Commands.UserCommands
{
	public class CreateCommand : IRequest<Result<UserViewModel>>
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public RoleType Role {get; set; }
	}
}

