using System;
using MediatR;
using Standard.ToLyst.Model.Aggregates.Markets;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Application.Commands.MarketCommands
{
	public class CreateCommand : Request, IRequest<Result<Market>>
	{
		public string Name { get; set; }
		public string BrandIcon { get; set; }
		public string BaseUrl { get; set; }
		public MarketType Type { get; set; }
	}
}

