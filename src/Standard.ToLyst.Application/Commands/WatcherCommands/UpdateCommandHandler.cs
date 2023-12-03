using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToLyst.Application.Extensions;
using Standard.ToLyst.Model.Aggregates.Watchers;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.Constants;

namespace Standard.ToLyst.Application.Commands.WatcherCommands
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, Result<Unit>>
    {
        private readonly IWatcherRepository _repository;

        public UpdateCommandHandler(IWatcherRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Unit>> Handle(UpdateCommand request, CancellationToken cancelationToken)
        {
            var result = new Result<Unit>(Unit.Value);
            var watcher = await _repository.GetOneAsync(request.Expression);

            if (watcher == null)
                return result.SetResult(ResultStatus.NotFound, Messages.NotFound.SetMessageValues("Watcher"));

            watcher.Update(request.Name, request.Desired);
            await _repository.UpdateAsync(it => it.Id == watcher.Id, watcher);

            return result.SetResult(ResultStatus.NoContent, null);
        }
    }
}