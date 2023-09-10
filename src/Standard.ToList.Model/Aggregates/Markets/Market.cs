using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Standard.ToList.Model.Aggregates.Products;

namespace Standard.ToList.Model.Aggregates.Markets
{
    public class Market : Entity
    {
		public string Name { get; set; }
		public string BrandIcon { get; set; }
		public string BaseUrl { get; set; }
		public MarketType? Type { get; set; } = MarketType.PingoDoce;

        protected HttpClient _httpClient { get; set; }

        public Market()
		{
		}

        public Market(string id)
        {
			Id = id;
        }

        public Market(string id, string name, MarketType? type, string baseUrl) : base(id)
        {
            Name = name;
            Type = type;
            BaseUrl = baseUrl;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

        public Market(MarketType type, string baseUrl) : base()
		{
			Type = type;
            BaseUrl = baseUrl;
			_httpClient = new HttpClient();
			_httpClient.BaseAddress = new Uri(BaseUrl);
        }
				
		public virtual async Task<IEnumerable<Product>> SearchAsync(string product)
		{
			return Array.Empty<Product>();
		}

		public virtual void Sleep()
		{
			int time = new Random().Next(1000, 3000);
			Thread.Sleep(time);
		}
	}
}

