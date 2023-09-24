using MediatR;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application.Commands.InstanceCommands
{
    public class CheckCommand : Request, IRequest<Result<Unit>>
	{
        public string InstanceId { get; set; }
        public string ProductId { get; set; }
        public bool Value { get; set; }
	}
}

