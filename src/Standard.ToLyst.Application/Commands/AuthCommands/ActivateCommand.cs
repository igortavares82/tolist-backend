using MediatR;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Application.Commands.AuthCommands
{
    public class ActivateCommand : IRequest<Result<Unit>>
	{
		public string Token { get; set; }
	}
}
