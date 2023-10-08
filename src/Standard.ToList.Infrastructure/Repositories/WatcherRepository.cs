using Microsoft.Extensions.Options;
using Standard.ToList.Model.Aggregates.Watchers;
using Standard.ToList.Model.Options;

namespace Standard.ToList.Infrastructure.Repositories
{
    public class WatcherRepository : Repository<Watcher>, IWatcherRepository
	{
		public WatcherRepository(IOptions<AppSettingOptions> settings) : base(settings)
		{
		}
	}
}
