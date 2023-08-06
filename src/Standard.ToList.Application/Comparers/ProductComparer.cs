using System.Collections.Generic;
using Standard.ToList.Model.Aggregates.Products;

namespace Standard.ToList.Application.Comparers
{
    public class ProductComparer : IEqualityComparer<Product>
	{
		public ProductComparer()
		{
		}

        public bool Equals(Product x, Product y)
        {
            return x.Name == y.Name && x.Market.Id == y.Market.Id;
        }

        public int GetHashCode(Product obj)
        {
            int? code = obj.Name?.GetHashCode() ^ obj.Market.Id?.GetHashCode();
            return code.GetHashCode();
        }
    }
}

