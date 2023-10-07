using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToList.Application.Extensions;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Aggregates.Users;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.Constants;

namespace Standard.ToList.Application.Commands.InstanceCommands
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, Result<Unit>>
    {
        private readonly ILystRepository _repository;

        public async Task<Result<Unit>> Handle(UpdateCommand request, CancellationToken cancelationToken)
        {
            var result = new Result<Unit>(Unit.Value);
            var lyst = await _repository.GetAsync(it => it.Id == request.ResourceId && (it.UserId == request.UserId || request.RoleType == RoleType.Admin));

            if (lyst == null)
                return result.SetResult(ResultStatus.NotFound, Messages.NotFound.SetMessageValues("Lyst"));


        }
    }
}