using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Aggregates.Products;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application.Commands.LystCommands
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand, Result<Lyst>>
    {
        private readonly ILystRepository _repository;

        public CreateCommandHandler(ILystRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Lyst>> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var lyst = new Lyst(request.Name, request.UserId, request.IsDraft, request.Items.Select(it => new Product(it)).ToArray());
            lyst = await _repository.CreateAsync(lyst);

            return new Result<Lyst>(lyst, ResultStatus.Success, "List created successfully.");
        }
    }
}

