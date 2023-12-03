using MediatR;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.ViewModels.Auth;

namespace Standard.ToLyst.Application.Commands.AuthCommands
{
    public class AuthCommand : IRequest<Result<AuthViewModel>>
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
