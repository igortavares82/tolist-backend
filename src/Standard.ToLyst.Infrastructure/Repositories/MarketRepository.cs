using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Standard.ToLyst.Model.Aggregates.Markets;
using Standard.ToLyst.Model.Options;

namespace Standard.ToLyst.Infrastructure.Repositories
{
	public class MarketRepository : Repository<Market>, IMarketRepository
	{
		public MarketRepository(IOptions<AppSettingOptions> settings) : base(settings)
		{
		}
    }
}

