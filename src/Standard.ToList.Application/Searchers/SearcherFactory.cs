using System;
using System.Net.Http;
using Standard.ToList.Model.Aggregates.Markets;

namespace Standard.ToList.Application.Searchers
{
    public class SearcherFactory
    {
        public Searcher Instance(Market market)
        {
            var httpClient = new HttpClient() { BaseAddress = new Uri(market.BaseUrl) };

            switch (market.Type)
			{
				case MarketType.PingoDoce:
					return new PingoDoceSearcher(market, httpClient);

				case MarketType.Auchan:
					return new AuchanSearcher(market, httpClient);

				default:
                    return new AuchanSearcher(market, httpClient);
            }
        }
    }
}
