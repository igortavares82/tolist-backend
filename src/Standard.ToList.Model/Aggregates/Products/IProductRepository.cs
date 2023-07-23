using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Standard.ToList.Model.Aggregates.Products
{
	public interface IProductRepository : IRepository<Product>
	{
		Task<IEnumerable<Product>> GetAsync(string marketId, string[] products);
	}
}
