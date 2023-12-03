using System;
namespace Standard.ToList.Model.Aggregates.Products
{
	public class MissingProduct : Entity
	{
		public string Name { get; set; }
		public string MarketId { get; set; }
		
		public MissingProduct(string name, string marketId)
		{
			Name = name;
			MarketId = marketId;
		}
	}
}

