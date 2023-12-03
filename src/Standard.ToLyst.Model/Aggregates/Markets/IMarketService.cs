using System.Threading.Tasks;
using Standard.ToLyst.Model.Aggregates.Configuration;
using Standard.ToLyst.Model.Aggregates.Products;

namespace Standard.ToLyst.Model.Aggregates.Markets
{
    public interface IMarketService
	{
		Task RegisterMissingProductAsync(MissingProduct[] missingProducts);
		Task<Worker> SearchMissingProductsAsync(Worker worker);
		Task<Worker> SearchOutdatedProductsAsync(Worker worker);
	}
}

