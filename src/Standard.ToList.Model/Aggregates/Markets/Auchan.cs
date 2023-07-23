using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.ToList.Model.Aggregates.Products;
using Standard.ToList.Model.SeedWork;

namespace Standard.ToList.Model.Aggregates.Markets
{
    public class Auchan : Market, IAggregateRoot
    {
        public Auchan() : base()
        {
            Type = MarketType.Auchan;
        }

        public override async Task<IEnumerable<Product>> SearchAsync(string product)
        {
            throw new NotImplementedException();
        }
    }
}

