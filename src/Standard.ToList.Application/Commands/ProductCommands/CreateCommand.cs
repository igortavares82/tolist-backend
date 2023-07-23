using System;
using MediatR;
using Standard.ToList.Model.Aggregates.Products;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application.Commands.ProductCommands
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

