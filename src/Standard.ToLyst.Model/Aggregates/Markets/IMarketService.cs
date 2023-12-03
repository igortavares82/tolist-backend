using System.Threading.Tasks;
using Standard.ToList.Model.Aggregates.Configuration;
using Standard.ToList.Model.Aggregates.Products;

namespace Standard.ToList.Model.Aggregates.Markets
{
    public interface IMarketService
	{
		Task RegisterMissingProductAsync(MissingProduct[] missingProducts);
		Task<Worker> SearchMissingProductsAsync(Worker worker);
		Task<Worker> SearchOutdatedProductsAsync(Worker worker);
	}
}

