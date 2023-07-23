using System;

namespace Standard.ToList.Model.Aggregates.Requests
{
	public class ProductRequest
	{
		public string[] MarketIds { get; set; }
		public string[] Names { get; set; }
	}
}

