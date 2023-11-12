using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.Aggregates.Products;

namespace Standard.ToList.Application.Searchers
{
    public class AuchanSearcher : Searcher
    {
        private const string URL = "pt/pesquisa?q={0}";
        private const string MATCHES = "<div class=\"auc-product-tile__name\"[^>]*>([\\s\\S]*?)<\\/div>";
        private const string DESC_REGEX = "<a class=\"link\"[^>]*>([\\s\\S]*?)<\\/a>";
        private const string PRICE_REGEX = "<span class=\"value\"[^>]*content=\"(\\d*.\\d*?)\">";
        
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
            var length = new Regex(MATCHES, RegexOptions.IgnoreCase).Match(html).Length;

            for (int i = 0; i < length; i++) 
            {
                try 
                {
                    string name = new Regex(DESC_REGEX, RegexOptions.IgnoreCase).Matches(html)[i].Groups[1].Value;
                    string price = new Regex(PRICE_REGEX, RegexOptions.IgnoreCase).Matches(html)[i].Groups[1].Value;

                    products.Add(new Product(name, this._market.Id, null, null, Convert.ToDecimal(price.Replace(".", ","))));
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                    continue;
                }
            }

            return products.AsEnumerable();
        }
    }
}
