using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Aggregates.Products;

namespace Standard.ToList.Model.Aggregates.Markets
{
	public class MiniPreco : Market
	{
		public MiniPreco() : base()
		{
            Type = MarketType.MiniPreco;
		}

        public override async Task<IEnumerable<Product>> SearchAsync(string product)
        {
            throw new NotImplementedException();
        }
    }
}

