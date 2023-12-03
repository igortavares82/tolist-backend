using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToLyst.Application.Extensions;
using Standard.ToLyst.Model.Aggregates.Lists;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.Constants;

namespace Standard.ToLyst.Application.Commands.InstanceCommands
{
    public class CheckCommandHandler : IRequestHandler<CheckCommand,Result<Unit>>
	{
        private readonly ILystRepository _repository;

        public CheckCommandHandler(ILystRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Unit>> Handle(CheckCommand request, CancellationToken cancellationToken)
        {
            var result = new Result<Unit>(Unit.Value);
            var lyst = await _repository.GetOneAsync(request.Query());

            if (lyst == null)
                return result.SetResult(ResultStatus.NotFound, Messages.NotFound.SetMessageValues("Instance"));

            lyst.Instances
                .FirstOrDefault(it => it.Id == request.InstanceId)
                .CheckProduct(request.ProductId, request.Value);

            await _repository.UpdateAsync(it => it.Id == lyst.Id, lyst);
            return result.SetResult(ResultStatus.NoContent, Messages.UpdatedSuccess.SetMessageValues("Product"));
        }
    }
}

