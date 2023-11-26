using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Standard.ToList.Model.Aggregates.Markets;

namespace Standard.ToList.Infrastructure.Searchers
{
    public class SearcherFactory
    {
        private readonly IServiceProvider _provider;

        public SearcherFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public Searcher Instance(Market market)
        {
            var httpClient = new HttpClient() 
            { 
                BaseAddress = new Uri(market.BaseUrl), 
                Timeout = TimeSpan.FromMinutes(1) 
            };

            var scope = _provider.CreateScope();

            switch (market.Type)
			{
				case MarketType.PingoDoce:
					return new PingoDoceSearcher(market, httpClient, scope);

				case MarketType.Auchan:
					return new AuchanSearcher(market, httpClient, scope);

                case MarketType.Continente:
					return new ContinenteSearcher(market, httpClient, scope);

				default:
                    return null;
            }
        }
    }
}
