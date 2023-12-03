using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToLyst.Model.Aggregates.Users;
using Standard.ToLyst.Model.Aggregates.Watchers;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Application.Commands.WatcherCommands
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, Result<Unit>>
    {
        private readonly IWatcherRepository _repository;

        public DeleteCommandHandler(IWatcherRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Unit>> Handle(DeleteCommand request, CancellationToken cancelationToken)
        {
            var result = new Result<Unit>(Unit.Value, ResultStatus.NoContent);
            await _repository.DeleteAsync(it => it.Id == request.ResourceId &&
                                                (it.UserId == request.UserId || request.RoleType == RoleType.Admin));

            return result;
        }
    }
}