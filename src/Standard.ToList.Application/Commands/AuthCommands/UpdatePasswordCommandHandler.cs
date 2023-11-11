
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToList.Application.Extensions;
using Standard.ToList.Application.Services;
using Standard.ToList.Model.Aggregates.Users;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.Constants;

namespace Standard.ToList.Application
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, Result<Unit>>
    {
        private readonly IUserRepository _repository;
        private readonly TokenService _tokenService;

        public UpdatePasswordCommandHandler(IUserRepository repository, TokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        public async Task<Result<Unit>> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var result = new Result<Unit>(Unit.Value);
            var user = await _repository.GetOneAsync(it => it.RetrieveToken == request.Token);

            if (user == null)
                return result.SetResult(ResultStatus.NotFound, Messages.NotFound.SetMessageValues("User"));

            if (!_tokenService.IsValid(request.Token))
                return result.SetResult(ResultStatus.UnprosseableEntity, Messages.InvalidToken);

            var salt = user.CreateDate.ToString().GetDateMask();
            var password = request.NewPassword.ToHash(salt);
            
            user.Update(password);
            await _repository.UpdateAsync(it => it.Id == user.Id, user);

            return result.SetResult(ResultStatus.NoContent, string.Empty);
        }
    }
}
