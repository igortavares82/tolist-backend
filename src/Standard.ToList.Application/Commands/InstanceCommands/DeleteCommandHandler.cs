using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToList.Application.Extensions;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.Constants;

namespace Standard.ToList.Application.Commands.InstanceCommands
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, Result<Unit>>
    {
        private readonly ILystRepository _repository;

        public DeleteCommandHandler(ILystRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Unit>> Handle(DeleteCommand request, CancellationToken cancelationToken)
        {
            var result = new Result<Unit>(Unit.Value);
            var lyst = await _repository.GetOneAsync(it => it.Id == request.ResourceId &&
                                                     it.UserId == request.UserId);

            if (lyst == null)
                return result.SetResult(ResultStatus.NotFound, Messages.NotFound.SetMessageValues("List"));

            lyst.DeleteInstance(request.InstanceId);
            await _repository.UpdateAsync(it => it.Id == lyst.Id, lyst);

            return result.SetResult(ResultStatus.NoContent, null);
        }
    }
}