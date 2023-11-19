using System;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Model.Aggregates.Requests
{
	public class ProductRequest : Request
	{
		public string[] MarketIds { get; set; }
		public string[] Names { get; set; }
		public bool FromSource { get; set; }

	}
}

