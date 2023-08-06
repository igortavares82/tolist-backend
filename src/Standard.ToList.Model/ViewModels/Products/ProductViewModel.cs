using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.Aggregates.Products;

namespace Standard.ToList.Model.ViewModels.Products
{
    public class ProductViewModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Picture { get; set; }
		public decimal Price { get; set; }
		public string MarketName { get; set; }
		
        public ProductViewModel(Product product, Market market)
		{
			Id = product.Id;
			Name = product.Name;
			Description = product.Description;
			Picture = product.Picture;
			Price = product.Price;
			MarketName = market?.Name;
		}

        public ProductViewModel(Product product) : this(product, null)
        {
           
        }
    }
}

