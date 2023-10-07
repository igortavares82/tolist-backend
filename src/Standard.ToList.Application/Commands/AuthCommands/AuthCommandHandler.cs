using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToList.Application.Services;
using Standard.ToList.Model.Aggregates.Users;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.ViewModels.Auth;
using Standard.ToList.Application.Extensions;
using Standard.ToList.Model.Constants;

namespace Standard.ToList.Application.Commands.AuthCommands
{
	public class AuthCommandHandler : IRequestHandler<AuthCommand,Result<AuthViewModel>>
	{
        private readonly IUserRepository _repository;
        private readonly TokenService _tokenService;

        public AuthCommandHandler(IUserRepository repository, TokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        public async Task<Result<AuthViewModel>> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            var result = new Result<AuthViewModel>(null, ResultStatus.NotFound, Messages.NotFound.SetMessageValues("User"));
            var user = await _repository.GetOneAsync(it => it.Email == request.Email);

            if (user == null)
                return result;

            var salt = user.CreateDate.ToString().GetDateMask();
            if (user.Password != request.Password.ToHash(salt))
                return result;

            if (!user.IsActive)
            {
                result.SetResult(ResultStatus.UnprosseableEntity, Messages.MustBeActivated, true);
                return result;
            }

            var token = _tokenService.GetToken(user);
            var expires = DateTime.UtcNow.AddDays(300);

            user.SetLastAccess();
            await _repository.UpdateAsync(it => it.Id == user.Id, user);

            return new Result<AuthViewModel>(new AuthViewModel(token, expires), ResultStatus.Success);
        }
    }
}

