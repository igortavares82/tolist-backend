using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Aggregates.Products;
using Newtonsoft.Json.Linq;
using System.Linq;
using Standard.ToList.Model.SeedWork;

namespace Standard.ToList.Model.Aggregates.Markets
{
	public class PingoDoce : Market, IAggregateRoot
    {
        private const string URL = "api/catalogues/6107d28d72939a003ff6bf51/products/search?query={0}&from=0&size=100&esPreference=0.43168016774115214";
        private string[] fields = { "firstName", "buyingPrice" };

		public PingoDoce() : base()
		{
		}

        public override async Task<IEnumerable<Product>> SearchAsync(string product)
        {
            using var httpResponse = await _httpClient.GetAsync(string.Format(URL, product));
            string json = await httpResponse.Content.ReadAsStringAsync();

            JObject jObject = JObject.Parse(json);
            var products = jObject.SelectToken($"$.sections.*.products")?.ToList();

            return products.Select(it =>
            {
                var name = it.SelectToken("$._source.firstName")?.Value<string>();
                var price = it.SelectToken("$._source.buyingPrice").Value<decimal>();
                var description = it.SelectToken("$._source.additionalInfo")?.Value<string>();

                return new Product(name, null, null, description, price);
            })
            .AsEnumerable();
        }
    }
}

