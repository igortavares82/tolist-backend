using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToList.Application.Extensions;
using Standard.ToList.Model.Aggregates.Users;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application.Commands.UserCommands
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, Result<Unit>>
	{
        private readonly IUserRepository _repository;

        public UpdateCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Unit>> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var result = new Result<Unit>(Unit.Value);
            var user = await _repository.GetOneAsync(it => it.Id == request.ResourceId);

            if (user == null)
                return result.SetResult(ResultStatus.NotFound, "User not found.");

            var createDate = user.CreateDate.ToString().GetDateMask();
            user.Update(request.Name, request.Password.ToHash(createDate));

            await _repository.UpdateAsync(it => it.Id == request.UserId, user);
            return result.SetResult(ResultStatus.NoContent, null);
        }
    }
}

