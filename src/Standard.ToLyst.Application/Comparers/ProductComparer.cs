using System.Collections.Generic;
using Standard.ToLyst.Model.Aggregates.Products;

namespace Standard.ToLyst.Application.Comparers
{
    public class ProductComparer : IEqualityComparer<Product>
	{
		public ProductComparer()
		{
		}

        public bool Equals(Product x, Product y)
        {
            return x.Name == y.Name && x.Id == y.Id;
        }

        public int GetHashCode(Product obj)
        {
            int? code = obj.Name?.GetHashCode() ^ obj.Id?.GetHashCode();
            return code.GetHashCode();
        }
    }
}

