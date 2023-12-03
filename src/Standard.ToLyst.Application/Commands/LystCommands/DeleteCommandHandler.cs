using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToLyst.Application.Extensions;
using Standard.ToLyst.Model.Aggregates.Lists;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Application.Commands.LystCommands
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, Result<Unit>>
    {
        private readonly ILystRepository _lystRepository;

        public DeleteCommandHandler(ILystRepository lystRepository)
        {
            _lystRepository = lystRepository;
        }

        public async Task<Result<Unit>> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            await _lystRepository.DeleteAsync(request.Query());
            return new Result<Unit>(Unit.Value);
        }
    }
}

