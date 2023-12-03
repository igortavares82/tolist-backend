using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToLyst.Application.Extensions;
using Standard.ToLyst.Model.Aggregates.Lists;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.Constants;

namespace Standard.ToLyst.Application.Commands.LystCommands
{
	public class UpdateCommandHandler : IRequestHandler<UpdateCommand, Result<Unit>>
	{
        private readonly ILystRepository _repository;

        public UpdateCommandHandler(ILystRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Unit>> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var lyst = await _repository.GetOneAsync(request.ResourceId);

            if (lyst == null)
                return new Result<Unit>(Unit.Value, ResultStatus.NotFound, Messages.NotFound.SetMessageValues("Lyst"));

            var items = request.Items.MapToProducts();
            lyst.Update(request.Name, request.IsDraft, request.IsEnabled, items);
            await _repository.UpdateAsync(lyst);

            return new Result<Unit>(Unit.Value);
        }
    }
}

