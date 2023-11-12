using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.Aggregates.Products;

namespace Standard.ToList.Application.Searchers
{
    public class PingoDoceSearcher : Searcher
    {
        private const string URL = "api/catalogues/6107d28d72939a003ff6bf51/products/search?query={0}&from=0&size=100&esPreference=0.43168016774115214";

        private readonly ILogger<PingoDoceSearcher> _logger;
        
        public PingoDoceSearcher(Market market, 
                                 HttpClient httpClient, 
                                 IServiceScope scope) : 
                            base(market, httpClient)
        {
            _logger = scope.ServiceProvider.GetRequiredService<ILogger<PingoDoceSearcher>>();
        }

        public override async Task<IEnumerable<Product>> SearchAsync(string product) 
        {
            base.Sleep();

            var products = new List<Product>();

            using var httpResponse = await _httpClient.GetAsync(string.Format(URL, product));
            string json = await httpResponse.Content.ReadAsStringAsync();

            JObject jObject = JObject.Parse(json);
            var _products = jObject.SelectToken($"$.sections.*.products")?.ToList();

            foreach(var _product in  _products)
            {
                try
                {
                    var name = _product.SelectToken("$._source.firstName")?.Value<string>();
                    var price = _product.SelectToken("$._source.buyingPrice").Value<decimal>();
                    var description = _product.SelectToken("$._source.additionalInfo")?.Value<string>();

                    products.Add(new Product(name, _market.Id, null, description, price));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                    continue;
                }
            }

            return products.AsEnumerable();
        }
    }
}
