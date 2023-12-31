﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Standard.ToLyst.Model.Aggregates.Markets;
using Standard.ToLyst.Model.Aggregates.Products;

namespace Standard.ToLyst.Infrastructure.Searchers
{
    public class PingoDoceSearcher : Searcher
    {
        private const string URL = "api/catalogues/6107d28d72939a003ff6bf51/products/search?query={0}&from=0&size=100&esPreference=0.43168016774115214";
        private const string MEDIA = "https://res.cloudinary.com/fonte-online/image/upload/c_fill,h_300,q_auto,w_300/v1/PDO_PROD/{0}_1";

        private readonly ILogger<PingoDoceSearcher> _logger;
        
        public PingoDoceSearcher(Market market, 
                                 HttpClient httpClient, 
                                 IServiceScope scope) : 
                            base(market, httpClient)
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36");
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
                    var brand =  _product.SelectToken("$._source.unit.name")?.Value<string>();
                    var unit = _product.SelectToken("$._source.capacity")?.Value<string>();
                    var media = string.Format(MEDIA, _product.SelectToken("$._source.sku")?.Value<string>());

                    if (!products.Any(it => it.Name == name))
                        products.Add(new Product(name, _market.Id, null, description, price, null, null, unit, media));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    continue;
                }
            }

            return products.AsEnumerable();
        }
    }
}
