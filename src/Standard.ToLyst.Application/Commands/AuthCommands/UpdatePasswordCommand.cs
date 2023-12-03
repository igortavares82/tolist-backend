using MediatR;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application
{
    public class UpdatePasswordCommand : IRequest<Result<Unit>>
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
