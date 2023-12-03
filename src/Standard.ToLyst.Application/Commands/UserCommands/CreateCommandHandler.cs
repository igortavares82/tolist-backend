using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Standard.ToLyst.Model.Aggregates.Users;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.Options;
using Standard.ToLyst.Application.Extensions;
using Standard.ToLyst.Model.ViewModels.Users;
using System.Linq;
using Standard.ToLyst.Application.Services;
using Standard.ToLyst.Model.Constants;

namespace Standard.ToLyst.Application.Commands.UserCommands
{
    // https://balta.io/artigos/aspnetcore-3-autenticacao-autorizacao-bearer-jwt
    public class CreateCommandHandler : IRequestHandler<CreateCommand, Result<UserViewModel>>
	{
        private readonly IUserRepository _repository;
        private readonly AppSettingOptions _settings;
        private readonly TokenService _tokenService;

        public CreateCommandHandler(IUserRepository repository,
                                    IOptions<AppSettingOptions> settings,
                                    TokenService tokenService)
        { 
            _repository = repository;
            _settings = settings.Value;
            _tokenService = tokenService;
        }

        public async Task<Result<UserViewModel>> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            if (!await _repository.CanRegisterAsync(request.Email))
                return new Result<UserViewModel>(null, ResultStatus.UnprosseableEntity, Messages.UserExists);
            

            if (!_settings.AllowedAdmins.Contains(request.Email) && request.Role == RoleType.Admin)
                return new Result<UserViewModel>(null, ResultStatus.Error, Messages.OperationNotAllowed);

            var createDate = DateTime.UtcNow;
            var user = new User(request.Name,
                                request.Password.ToHash(createDate.ToString().GetDateMask()),
                                request.Email,
                                createDate,
                                request.Role);

            user.SetActivationToken(_tokenService.GetToken(user));
            await _repository.CreateAsync(user);

            return new Result<UserViewModel>(new UserViewModel(user), ResultStatus.Success);
        }
    }
}

