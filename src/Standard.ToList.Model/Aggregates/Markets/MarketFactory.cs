using System;
namespace Standard.ToList.Model.Aggregates.Markets
{
	public class MarketFactory
	{
		public Market Instance(Market market)
		{
			switch (market.Type)
			{
				case MarketType.PingoDoce:
					return new PingoDoce(market.Id, market.Name, market.Type, market.BaseUrl);

				case MarketType.MiniPreco:
					return new MiniPreco();

				default:
                    return new Auchan();
            }
		}
	}
}

