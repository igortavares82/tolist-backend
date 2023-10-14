using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Watcher>> GetAsync(int interval)
        {
            var watchers = await base.GetAsync(it => it.IsEnabled == true &&
                                                     (!it.LastSentMessageDate.HasValue || (DateTime.UtcNow.Day - it.LastSentMessageDate.Value.Day >= interval)));

            return watchers;
        }
    }
}
