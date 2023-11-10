
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToList.Application.Extensions;
using Standard.ToList.Model.Aggregates.Users;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.Constants;

namespace Standard.ToList.Application
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, Result<Unit>>
    {
        private readonly IUserRepository _repository;

        public UpdatePasswordCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Unit>> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var resutl = new Result<Unit>(Unit.Value);
            var user = await _repository.GetOneAsync(it => it.ActivationToken == request.Token);

            if (user == null)
                return resutl.SetResult(ResultStatus.NotFound, Messages.NotFound.SetMessageValues("User"));

            


            return resutl;
        }
    }
}
