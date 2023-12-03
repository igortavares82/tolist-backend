using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Standard.ToLyst.Application.Extensions;
using Standard.ToLyst.Application.Services;
using Standard.ToLyst.Model.Aggregates.Users;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.Constants;

namespace Standard.ToLyst.Application
{
    public class RetrieveCommandHandler : IRequestHandler<RetrieveCommand, Result<Unit>>
    {
        private readonly IUserRepository _repository;
        private readonly TokenService _tokenService;
        private readonly ILogger<RetrieveCommandHandler> _logger;

        public RetrieveCommandHandler(IUserRepository repository, 
                                      TokenService tokenService, 
                                      ILogger<RetrieveCommandHandler> logger)
        {
            _repository = repository;
            _tokenService = tokenService;
            _logger = logger;
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
            await _repository.UpdateAsync(it => it.Id == user.Id, user);

            return result.SetResult(ResultStatus.NoContent, string.Empty);
        }
    }
}
