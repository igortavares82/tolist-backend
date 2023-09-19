using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Standard.ToList.Model.Aggregates.Users;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.Options;
using Standard.ToList.Application.Extensions;
using Standard.ToList.Model.ViewModels.Users;

namespace Standard.ToList.Application.Commands.UserCommands
{
    // https://balta.io/artigos/aspnetcore-3-autenticacao-autorizacao-bearer-jwt
    public class CreateCommandHandler : IRequestHandler<CreateCommand, Result<UserViewModel>>
	{
        private readonly IUserRepository _repository;
        private readonly AppSettingOptions _settings;

        public CreateCommandHandler(IUserRepository repository,
                                    IOptions<AppSettingOptions> settings)
        {
            _repository = repository;
            _settings = settings.Value;
        }

        public async Task<Result<UserViewModel>> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            if (!await _repository.CanRegisterAsync(request.Email))
            {
                return new Result<UserViewModel>(null, ResultStatus.Exists, "User already exists.");
            }

            var createDate = DateTime.UtcNow;
            var user = new User(request.Name,
                                request.Password.ToHash(createDate.ToString().GetDateMask()),
                                request.Email,
                                createDate,
                                request.Role);

            await _repository.CreateAsync(user);

            return new Result<UserViewModel>(new UserViewModel(user), ResultStatus.Success);
        }
    }
}

