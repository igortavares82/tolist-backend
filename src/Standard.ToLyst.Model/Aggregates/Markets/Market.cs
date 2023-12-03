using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Standard.ToList.Model.Aggregates.Products;
using Standard.ToList.Model.SeedWork;

namespace Standard.ToList.Model.Aggregates.Markets
{
    public class Market : Entity, IAggregateRoot
    {
		public string Name { get; set; }
		public string BrandIcon { get; set; }
		public string BaseUrl { get; set; }
		public MarketType? Type { get; set; }

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
        }

        public Market(MarketType type, string baseUrl) : base()
		{
			Type = type;
            BaseUrl = baseUrl;
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

