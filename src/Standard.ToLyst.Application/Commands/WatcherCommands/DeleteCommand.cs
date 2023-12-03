using MediatR;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application.Commands.WatcherCommands
{
    public class DeleteCommand : Request, IRequest<Result<Unit>>
    {
    }
}
