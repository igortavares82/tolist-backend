using System;
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
    public class RetrieveCommandHandler : IRequestHandler<RetrieveCommand, Result<Unit>>
    {
        private readonly IUserRepository _repository;
        private readonly TokenService _tokenService;

        public RetrieveCommandHandler(IUserRepository repository, TokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        public async Task<Result<Unit>> Handle(RetrieveCommand request, CancellationToken cancellationToken)
        {
            var result = new Result<Unit>(Unit.Value);
            var user = await _repository.GetOneAsync(it => it.Email == request.Email);

            if (user == null)
                return result.SetResult(ResultStatus.NotFound, Messages.NotFound.SetMessageValues("User"));

            var token = _tokenService.GetToken(user, 0.5);
            user.SetRetrieveToken(token);

            user.AddNotification(new RetrievedPasswordEvent(user));
            await _repository.UpdateAsync(user);

            return result;
        }
    }
}
