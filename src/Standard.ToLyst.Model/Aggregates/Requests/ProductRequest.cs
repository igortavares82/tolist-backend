using System;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Model.Aggregates.Requests
{
	public class ProductRequest : Request
	{
		public string[] MarketIds { get; set; }
		public string[] Names { get; set; }
		public bool SetAsMissingProduct { get; set; }
	}
}

