using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Standard.ToLyst.Infrastructure.Extensions;
using Standard.ToLyst.Model.Aggregates.Markets;
using Standard.ToLyst.Model.Aggregates.Products;
using Standard.ToLyst.Model.Constants;

namespace Standard.ToLyst.Infrastructure.Searchers
{
    public class AuchanSearcher : Searcher
    {
        private const string URL = "pt/pesquisa?q={0}";
        private const string MEDIA = "https://www.auchan.pt/dw/image/v2/BFRC_PRD/on/demandware.static/-/Sites-auchan-pt-master-catalog/default/dwa6a21612/images/hi-res/{0}.jpg?sw=250&amp;sh=250&amp;sm=fit&amp;bgcolor=FFFFFF";
        private string _pattern = $@"{RegexPatterns.SEARCHER_AUCHAN_NAME}|{RegexPatterns.SEARCHER_AUCHAN_PRICE}|{RegexPatterns.SEARCHER_AUCHAN_UNIT}|{RegexPatterns.SEARCHER_AUCHAN_PID}";
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
            var matches = new Regex(_pattern, RegexOptions.IgnoreCase).Matches(html);

            for (int i = 0; i < matches.Count; i = i + 4)
            {
                try 
                {
                    var productId = Match(matches, i, RegexPatterns.SEARCHER_AUCHAN_PID).ToPadLeft(9);
                    var name = Match(matches, i + 1, RegexPatterns.SEARCHER_AUCHAN_NAME).ToStr();

                    if (string.IsNullOrEmpty(name))
                        continue;

                    var unit = Match(matches, i + 2, RegexPatterns.SEARCHER_AUCHAN_UNIT).ToStr(); 
                    var price = Match(matches, i + 3, RegexPatterns.SEARCHER_AUCHAN_PRICE).ToDecimal("en-US");
                    
                    if (price == 0)
                        price = Match(matches, i + 3, RegexPatterns.SEARCHER_AUCHAN_PRICE).ToDecimal("en-US"); 

                    var media = string.Format(MEDIA, productId);

                    products.Add(new Product(name, _market.Id, null, null, price, null, null, unit, media));
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
