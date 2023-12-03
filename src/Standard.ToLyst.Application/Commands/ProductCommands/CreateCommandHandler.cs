using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToLyst.Model.Aggregates.Products;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Application.Commands.ProductCommands
{
	public class CreateCommandHandler : IRequestHandler<CreateCommand, Result<Product>>
	{
        private readonly IProductRepository _repository;

		public CreateCommandHandler(IProductRepository repository)
		{
            _repository = repository;
		}

        public async Task<Result<Product>> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name, request.MarketId, request.MarketId, request.Description, request.Price, null, null, null, null);
            await _repository.CreateAsync(product);

            return new Result<Product>(product, ResultStatus.Success, "Product created successfully.");
        }
    }
}

