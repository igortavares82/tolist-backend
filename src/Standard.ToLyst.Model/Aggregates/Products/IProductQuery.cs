using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.ToLyst.Model.Aggregates.Products;
using Standard.ToLyst.Model.Aggregates.Requests;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.ViewModels.Products;

namespace Standard.ToLyst.Model.Aggregates.Lists
{
	public interface IProductQuery
	{
		Task<Result<IEnumerable<Product>>> GetAsync();
        Task<Result<ProductSearchViewModel>> GetAsync(ProductRequest request);
    }
}

