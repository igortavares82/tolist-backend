using MediatR;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application.Commands.InstanceCommands
{
    public class DeleteCommand : Request, IRequest<Result<Unit>>
    {
        public string InstanceId { get; set; }
    }
}
