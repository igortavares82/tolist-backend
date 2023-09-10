using System.Collections.Generic;
using System.Linq;
using Standard.ToList.Model.Aggregates.Products;

namespace Standard.ToList.Application.Extensions
{
    public static class DictionaryExtension
	{
		public static Product[] MapToProducts(this Dictionary<string, string[]> source)
        {
			return source.SelectMany(it => it.Value.Select(_it => new Product(_it, it.Key))).ToArray();
        }
	}
}

