using MediatR;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Application.Commands.WatcherCommands
{
    public class DeleteCommand : Request, IRequest<Result<Unit>>
    {
    }
}
