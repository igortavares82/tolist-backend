using System.Collections.Generic;
using System.Threading.Tasks;

namespace Standard.ToLyst.Model.Aggregates.Watchers
{
    public interface IWatcherRepository : IRepository<Watcher>
	{
		Task<IEnumerable<Watcher>> GetAsync(int interval);
	}
}

