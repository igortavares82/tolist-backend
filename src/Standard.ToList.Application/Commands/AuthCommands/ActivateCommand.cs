using MediatR;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application.Commands.AuthCommands
{
    public class ActivateCommand : IRequest<Result<Unit>>
	{
		public string Token { get; set; }
	}
}
