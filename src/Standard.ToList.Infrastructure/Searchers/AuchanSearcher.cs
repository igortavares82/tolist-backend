using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Standard.ToList.Infrastructure.Extensions;
using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.Aggregates.Products;
using Standard.ToList.Model.Constants;

namespace Standard.ToList.Infrastructure.Searchers
{
    public class AuchanSearcher : Searcher
    {
        private const string URL = "pt/pesquisa?q={0}";
        
        private readonly ILogger<AuchanSearcher> _logger;

        public AuchanSearcher(Market market, 
                              HttpClient httpClient, 
                              IServiceScope scope) : 
                         base(market, httpClient)
        {
            _logger = scope.ServiceProvider.GetRequiredService<ILogger<AuchanSearcher>>();
        }

        public override async Task<IEnumerable<Product>> SearchAsync(string product) 
        {
            base.Sleep();

            var products = new List<Product>();

            using var httpResponse = await _httpClient.GetAsync(string.Format(URL, product));
            var html = await httpResponse.Content.ReadAsStringAsync();
            var matches = new Regex(RegexPatterns.SEARCHER_AUCHAN_MATCHES, RegexOptions.IgnoreCase).Matches(html);

            for (int i = 0; i < matches.Count; i = i + 2)
            {
                try 
                {
                    string name = Match(matches, i, RegexPatterns.SEARCHER_AUCHAN_NAME).Cleanup();
                    decimal price = Match(matches, i + 1, RegexPatterns.SEARCHER_AUCHAN_PRICE).ToDecimal("en-US");

                    if (price == 0)
                        price = Match(matches, i + 2, RegexPatterns.SEARCHER_AUCHAN_PRICE).ToDecimal("en-US");

                    if (!string.IsNullOrEmpty(name))
                        products.Add(new Product(name, _market.Id, null, null, price));
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
