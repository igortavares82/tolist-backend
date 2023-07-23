using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
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
            var builders = Builders<Product>.Filter;
            var filter = builders.Eq(it => it.MarketId, marketId) &
                         builders.Eq(it => it.IsEnabled, true) &
                         builders.Or(names.ToList().Select(it => builders.Regex(_it => _it.Name, $"(?i)(^{it}.*)")).ToArray());

            var products = await base.Collection.FindAsync<Product>(filter);
            return products.ToList();
        }
    }
}
