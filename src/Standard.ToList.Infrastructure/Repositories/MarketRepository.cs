using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.Options;

namespace Standard.ToList.Infrastructure.Repositories
{
	public class MarketRepository : Repository<Market>, IMarketRepository
	{
		public MarketRepository(IOptions<AppSettingOptions> settings) : base(settings)
		{
		}
    }
}

