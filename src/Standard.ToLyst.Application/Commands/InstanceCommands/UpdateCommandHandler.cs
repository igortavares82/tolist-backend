using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToLyst.Application.Extensions;
using Standard.ToLyst.Model.Aggregates.Lists;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.Constants;

namespace Standard.ToLyst.Application.Commands.InstanceCommands
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, Result<Unit>>
    {
        private readonly ILystRepository _repository;

        public UpdateCommandHandler(ILystRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Unit>> Handle(UpdateCommand request, CancellationToken cancelationToken)
        {
            var result = new Result<Unit>(Unit.Value);
            var lyst = await _repository.GetOneAsync(request.Query());

            if (lyst == null)
                return result.SetResult(ResultStatus.NotFound, Messages.NotFound.SetMessageValues("Lyst"));

            lyst.UpdateInstance(request.InstanceId, request.Name, request.IsEnabled);
            await _repository.UpdateAsync(it => it.Id == request.ResourceId, lyst);

            return result.SetResult(ResultStatus.NoContent, string.Empty);
        }
    }
}