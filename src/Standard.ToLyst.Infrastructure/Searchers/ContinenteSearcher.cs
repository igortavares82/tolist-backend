using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Standard.ToList.Infrastructure.Extensions;
using Standard.ToList.Infrastructure.Searchers;
using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.Aggregates.Products;
using Standard.ToList.Model.Constants;

namespace Standard.ToList.Infrastructure
{
    public class ContinenteSearcher : Searcher
    {
        private const string URL = "/on/demandware.store/Sites-continente-Site/default/Search-UpdateGrid?cgid=col-produtos&q={0}&pmin=0%2e01&srule=Continente%2008&start=0&sz=500";
        private string _pattern = $@"{RegexPatterns.SEARCHER_CONTINENTE_NAME}|{RegexPatterns.SEARCHER_CONTINENTE_BRAND}|{RegexPatterns.SEARCHER_CONTINENTE_PRICE}|{RegexPatterns.SEARCHER_CONTINENTE_UNIT}|{RegexPatterns.SEARCHER_CONTINENTE_MEDIA}";
        private readonly ILogger<ContinenteSearcher> _logger;

        public ContinenteSearcher(Market market, 
                                  HttpClient httpClient, 
                                  IServiceScope scope) : 
                             base(market, httpClient)
        {
            _logger = scope.ServiceProvider.GetRequiredService<ILogger<ContinenteSearcher>>();
        }

        public override async Task<IEnumerable<Product>> SearchAsync(string product) 
        {
            //base.Sleep();

            var products = new List<Product>();

            using var httpResponse = await _httpClient.GetAsync(string.Format(URL, product));
            var html = await httpResponse.Content.ReadAsStringAsync();
            var matches = new Regex(_pattern, RegexOptions.IgnoreCase).Matches(html);

            for(int i = 0; i < matches.Count; i = i + 6)
            {
                try
                {
                    var name = Match(matches, i, RegexPatterns.SEARCHER_CONTINENTE_NAME).ToStr();

                    if (string.IsNullOrEmpty(name))
                        continue;

                    var brand = Match(matches, i + 1, RegexPatterns.SEARCHER_CONTINENTE_BRAND).ToStr();
                    var price = Match(matches, i + 2, RegexPatterns.SEARCHER_CONTINENTE_PRICE).ToDecimal("en-US");
                    var unit = Match(matches, i + 3, RegexPatterns.SEARCHER_CONTINENTE_UNIT).ToStr();
                    var media = Match(matches, i + 4, RegexPatterns.SEARCHER_CONTINENTE_MEDIA);

                    if (!string.IsNullOrEmpty(name))
                        products.Add(new Product($"{name} - {brand}", _market.Id, null, null, price, null, brand, unit, media));
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    continue;
                }
            }

            return products.AsEnumerable();
        }
    }
}
