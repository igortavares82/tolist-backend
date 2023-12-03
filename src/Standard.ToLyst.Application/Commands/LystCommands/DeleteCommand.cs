using MediatR;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Application.Commands.LystCommands
{
    public class DeleteCommand : Request, IRequest<Result<Unit>>
    {
	}
}
