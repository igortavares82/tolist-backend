using MediatR;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.ViewModels.Auth;

namespace Standard.ToList.Application.Commands.AuthCommands
{
    public class AuthCommand : IRequest<Result<AuthViewModel>>
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
