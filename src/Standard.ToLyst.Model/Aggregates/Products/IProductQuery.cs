using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.ToList.Model.Aggregates.Products;
using Standard.ToList.Model.Aggregates.Requests;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.ViewModels.Products;

namespace Standard.ToList.Model.Aggregates.Lists
{
	public interface IProductQuery
	{
		Task<Result<IEnumerable<Product>>> GetAsync();
        Task<Result<ProductSearchViewModel>> GetAsync(ProductRequest request);
    }
}

