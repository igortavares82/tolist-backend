﻿using System;
using Standard.ToList.Model.Aggregates.Lists;

namespace Standard.ToList.Model.Aggregates.Markets
{
	public class Auchan : Market
	{
		public Auchan() : base()
		{
            Type = MarketType.Auchan;
		}

        public override Item SearchProduct(string product)
        {
            throw new NotImplementedException();
        }
    }
}

