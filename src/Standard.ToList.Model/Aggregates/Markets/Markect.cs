using System;
using Standard.ToList.Model.Aggregates.Lists;

namespace Standard.ToList.Model.Aggregates.Markets
{
	public abstract class Market : Entity
	{
		public string Name { get; set; }
		public string BrandIcon { get; set; }
		public string BaseUrl { get; set; }

		public Market() : base() { }

        public abstract Item SearchProduct(string product);
	}
}

