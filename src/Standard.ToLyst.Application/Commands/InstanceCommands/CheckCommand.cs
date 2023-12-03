using MediatR;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Application.Commands.InstanceCommands
{
    public class CheckCommand : Request, IRequest<Result<Unit>>
	{
        public string InstanceId { get; set; }
        public string ProductId { get; set; }
        public bool Value { get; set; }
	}
}

