using System;
using Standard.ToList.Model.Aggregates.Lists;

namespace Standard.ToList.Model.Aggregates.Markets
{
	public class Market : Entity
	{
		public string Name { get; set; }
		public string BrandIcon { get; set; }
		public string BaseUrl { get; set; }
		public MarketType Type { get; set; }

        public Market()
		{

		}

        public Market(string name, string baseUrl) : base()
		{
            Name = name;
            BaseUrl = baseUrl;
        }

		public virtual Item SearchProduct(string product)
		{
			throw new NotImplementedException();
		}
	}
}

