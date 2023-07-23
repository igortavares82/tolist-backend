using System;
namespace Standard.ToList.Model.Aggregates.Products
{
	public class Product : Entity
	{
		public string Name { get; set; }
		public string MarketId { get; set; }
		public string Picture { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }

		public Product(string name, string marketId, string picture, string description, decimal price)
		{
			Name = name;
			MarketId = marketId;
			Picture = picture;
			Description = description;
			Price = price;
		}
	}
}

