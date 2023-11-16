using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Model.Aggregates.Products
{
	public interface IProductRepository : IRepository<Product>
	{
		Task<IEnumerable<Product>> GetAsync(string marketId, string[] products);
		Task<IEnumerable<Product>> GetAsync(string[] marketIds, string[] names, Page page, Order order);
		Task UpdateAsync(Product[] products);
		Task<IEnumerable<Product>> GetAsync(string marketId, int maxOutdated, int limit);
		Task WatchAsync();
	}
}
