using MediatR;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Application
{
    public class UpdatePasswordCommand : IRequest<Result<Unit>>
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
