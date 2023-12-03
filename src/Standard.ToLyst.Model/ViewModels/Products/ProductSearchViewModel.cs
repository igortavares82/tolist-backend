using System;
using System.Linq;
using Standard.ToLyst.Model.Aggregates.Markets;
using Standard.ToLyst.Model.Aggregates.Products;

namespace Standard.ToLyst.Model.ViewModels.Products
{
	public class ProductSearchViewModel
	{
		public ProductViewModel[] Products { get; set; }
		public string[] NotFound { get; set; }

        public ProductSearchViewModel(Product[] products, Market[] markets, string[] notFound)
        {
            Products = products.Select(it => new ProductViewModel(it, markets.FirstOrDefault(_it => _it.Id == it.Market.Id))).ToArray();
            NotFound = notFound;
        }
    }
}

