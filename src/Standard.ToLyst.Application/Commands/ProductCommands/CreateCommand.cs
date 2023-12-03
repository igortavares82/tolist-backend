using System;
using MediatR;
using Standard.ToLyst.Model.Aggregates.Products;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Application.Commands.ProductCommands
{
	public class CreateCommand : IRequest<Result<Product>>
	{
        public string Name { get; set; }
        public string MarketId { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}

