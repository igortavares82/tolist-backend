using System;
using System.Collections.Generic;
using Standard.ToList.Model.Aggregates.Markets;

namespace Standard.ToList.Model.Aggregates.Products
{
	public class Product : Entity
	{
		public string Name { get; set; }
		public string Picture { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public Market Market { get; set; }

        public Product()
		{
		}

		public Product(string id, string marketId)
		{
			Id = id;
			Market = new Market(marketId);
		}

		public Product(string name, string marketId, string picture, string description, decimal price)
		{
			Name = name;
			Market = new Market(marketId);
            Picture = picture;
			Description = description;
			Price = price;
		}

		public void Update(string name, string description, decimal price)
		{
			Name = name;
			Description = description;
			Price = price;
			LastUpdate = DateTime.Now;
		}

		public void SetEnabled(bool isEnabled)
		{
			IsEnabled = isEnabled;
			LastUpdate = DateTime.UtcNow;
		}
	}
}

