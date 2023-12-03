using System.Collections.Generic;
using System.Threading.Tasks;

namespace Standard.ToList.Model.Aggregates.Watchers
{
    public interface IWatcherRepository : IRepository<Watcher>
	{
		Task<IEnumerable<Watcher>> GetAsync(int interval);
	}
}

