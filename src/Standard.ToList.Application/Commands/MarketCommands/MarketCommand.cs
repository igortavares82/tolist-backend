using System;
using MediatR;
using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application.Commands.MarketCommands
{
	public class MarketCommand : IRequest<Result<Market>>
	{
		public string Name { get; set; }
		public string BrandIcon { get; set; }
		public string BaseUrl { get; set; }
	}
}

