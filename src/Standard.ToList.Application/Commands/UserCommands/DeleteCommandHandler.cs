using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToList.Model.Aggregates.Users;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.Constants;

namespace Standard.ToList.Application.Commands.UserCommands
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, Result<Unit>>
	{
        private readonly IUserRepository _repository;

        public DeleteCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Unit>> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var result = new Result<Unit>(Unit.Value);
            var user = await _repository.GetOneAsync(it => it.Id == request.ResourceId);

            if (user == null)
                return result.SetResult(ResultStatus.NotFound, Validations.UserNotFound);

            await _repository.DeleteAsync(it => it.Id == user.Id);
            return result.SetResult(ResultStatus.NoContent, null);
        }
    }
}
