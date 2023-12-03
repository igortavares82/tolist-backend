using MediatR;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application.Commands.LystCommands
{
    public class DeleteCommand : Request, IRequest<Result<Unit>>
    {
	}
}
