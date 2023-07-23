using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Standard.ToList.Model.Aggregates;
using Standard.ToList.Model.Aggregates.Products;
using Standard.ToList.Model.Options;

namespace Standard.ToList.Infrastructure.Repositories
{
	public class ProductRepository : Repository<Product>, IProductRepository
    {
		public ProductRepository(IOptions<AppSettingOptions> settings) : base(settings)
		{
		}

        public async Task<IEnumerable<Product>> GetAsync(string marketId, string[] names)
        {
            List<Product> result = new List<Product>();
            var filter = Builders<Product>.Filter;

            foreach (var name in names)
            {
                var products = await base.Collection
                                         .FindAsync<Product>(filter.StringIn(it => it.Name, name) &
                                                             filter.Eq(it => it.IsEnabled, true));

                result.AddRange(products.ToList());
            }

            return result;
        }
    }
}
