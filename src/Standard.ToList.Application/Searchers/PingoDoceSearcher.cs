using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.Aggregates.Products;

namespace Standard.ToList.Application.Searchers
{
    public class PingoDoceSearcher : Searcher
    {
        private const string URL = "api/catalogues/6107d28d72939a003ff6bf51/products/search?query={0}&from=0&size=100&esPreference=0.43168016774115214";

        public PingoDoceSearcher(Market market, HttpClient httpClient) : base(market, httpClient)
        {
            _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.32.2");
            _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "utf-8");
        }

        public override async Task<IEnumerable<Product>> SearchAsync(string product) 
        {
            base.Sleep();

            using var httpResponse = await _httpClient.GetAsync(string.Format(URL, product));
            string json = await httpResponse.Content.ReadAsStringAsync();

            JObject jObject = JObject.Parse(json);
            var products = jObject.SelectToken($"$.sections.*.products")?.ToList();

            return products.Select(it =>
            {
                try
                {
                    var name = it.SelectToken("$._source.firstName")?.Value<string>();
                    var price = it.SelectToken("$._source.buyingPrice").Value<decimal>();
                    var description = it.SelectToken("$._source.additionalInfo")?.Value<string>();

                    return new Product(name, _market.Id, null, description, price);
                }
                catch (Exception ex)
                {
                    // TODO: log exception here.
                    return null;
                }
            })
            .Where(it => it != null)
            .AsEnumerable();
        }
    }
}
