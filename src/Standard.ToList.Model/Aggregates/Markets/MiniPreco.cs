﻿using System;
using Standard.ToList.Model.Aggregates.Lists;

namespace Standard.ToList.Model.Aggregates.Markets
{
	public class MiniPreco : Market
	{
		public MiniPreco() : base()
		{
		}

        public override Item SearchProduct(string product)
        {
            throw new NotImplementedException();
        }
    }
}
