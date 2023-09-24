using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToList.Application.Extensions;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.Constants;

namespace Standard.ToList.Application.Commands.InstanceCommands
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
            var lyst = await _repository.GetOneAsync(it => it.Id == request.ResourceId &&
                                                           it.UserId == request.UserId);

            if (lyst == null)
                return result.SetResult(ResultStatus.NotFound, Messages.NotFound.SetMessageValues("Instance"));

            lyst.Instances
                .FirstOrDefault(it => it.Id == request.InstanceId)
                .CheckProduct(request.ProductId, request.Value);

            await _repository.UpdateAsync(it => it.Id == lyst.Id, lyst);
            return result.SetResult(ResultStatus.NoContent, Messages.UpdatedSucces.SetMessageValues("Product"));
        }
    }
}

