using System.Threading.Tasks;
using Standard.ToList.Model.Aggregates.Configuration;

namespace Standard.ToList.Model.Aggregates.Markets
{
    public interface IMarketService
	{
		Task<Worker> SearchMissingProductsAsync(Worker worker);
		Task<Worker> SearchOutdatedProductsAsync(Worker worker);
	}
}

