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
        private const string MATCHES = "<div class=\"auc-product-tile__[name|prices]*\"[^>]*>([\\s\\S]*?)<\\/div>";
        private const string NAME_REGEX = "<a class=\"link\"[^>]*>([\\s\\S]*?)<\\/a>";
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
            var matches = new Regex(MATCHES, RegexOptions.IgnoreCase).Matches(html);

            for (int i = 0; i < matches.Count; i = i + 2) 
            {
                try 
                {
                    string name = Regex.Match(matches[i].Value, NAME_REGEX).Groups.LastOrDefault()?.Value; //new Regex(NAME_REGEX, RegexOptions.IgnoreCase).Matches(html)[i].Groups[1].Value;
                    string price =  Regex.Match(matches[i + 1].Value, PRICE_REGEX).Groups.LastOrDefault()?.Value.Replace(".", ","); //new Regex(PRICE_REGEX, RegexOptions.IgnoreCase).Matches(html)[i + 1].Groups[1].Value.Replace(".", ",");

                    products.Add(new Product(name, _market.Id, null, null, Convert.ToDecimal(price)));
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
