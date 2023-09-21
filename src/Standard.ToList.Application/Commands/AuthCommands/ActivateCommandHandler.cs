using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToList.Model.Aggregates.Users;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application.Commands.AuthCommands
{
    public class ActivateCommandHandler : IRequestHandler<ActivateCommand,Result<Unit>>
	{
        private readonly IUserRepository _repository;

        public ActivateCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Unit>> Handle(ActivateCommand request, CancellationToken cancellationToken)
        {
            var result = new Result<Unit>(Unit.Value);
            var user = await _repository.GetOneAsync(it => it.ActivationToken == request.Token);

            if (user == null)
                return result.SetResult(ResultStatus.NotFound, "Resource not found.");

            user.SetAsActive();

            await _repository.UpdateAsync(it => it.Id == user.Id, user);
            return result.SetResult(ResultStatus.Success, "User activated.");
        }
    }
}

