using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Standard.ToList.Model.Aggregates.Products;
using Standard.ToList.Model.SeedWork;

namespace Standard.ToList.Model.Aggregates.Markets
{
    public class PingoDoce : Market, IAggregateRoot
    {
        private const string URL = "api/catalogues/6107d28d72939a003ff6bf51/products/search?query={0}&from=0&size=100&esPreference=0.43168016774115214";
        private string[] fields = { "firstName", "buyingPrice" };

		public PingoDoce(string id, string name, MarketType? type, string baseUrl) : base(id, name, type, baseUrl)
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

                    return new Product(name, this.Id, null, description, price);
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

