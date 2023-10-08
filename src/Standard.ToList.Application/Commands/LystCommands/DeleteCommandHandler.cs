using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToList.Application.Extensions;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application.Commands.LystCommands
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

