using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
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
            
            var watchers = Collection.Find(it => it.IsEnabled == true &&
                                                 (!it.LastSentMessageDate.HasValue || (DateTime.UtcNow.Day - it.LastSentMessageDate.Value.Day >= interval)) &&
                                                 (it.Current < it.Price || it.Current <= it.Desired))
                                     .Limit(_settings.Workers.WatcherWorker.IterationQuantity);

            return watchers.ToEnumerable();
        }
    }
}
