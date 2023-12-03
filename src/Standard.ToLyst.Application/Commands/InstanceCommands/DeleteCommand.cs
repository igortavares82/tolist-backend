using MediatR;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Application.Commands.InstanceCommands
{
    public class DeleteCommand : Request, IRequest<Result<Unit>>
    {
        public string InstanceId { get; set; }
    }
}
