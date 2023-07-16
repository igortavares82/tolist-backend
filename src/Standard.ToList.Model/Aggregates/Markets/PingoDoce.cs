using System;
using Standard.ToList.Model.Aggregates.Lists;

namespace Standard.ToList.Model.Aggregates.Markets
{
	public class PingoDoce : Market
	{
		public PingoDoce() : base()
		{
            Type = MarketType.PingoDoce;
		}

        public override Item SearchProduct(string product)
        {
            throw new NotImplementedException();
        }
    }
}

