using System;
using System.Threading.Tasks;

namespace Standard.ToList.Model.Aggregates.Markets
{
	public interface IMarketService
	{
		Task SearchMissingProductsAsync();
	}
}

