using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.ViewModels.Watchers;

namespace Standard.ToList.Model.Aggregates.Watchers
{
    public interface IWatcherQuery
	{
		Task<Result<WatcherViewModel>> GetAsync(Request request);
        Task<Result<IEnumerable<WatcherViewModel>>> GetAsync(WatcherRequest request);
    }
}

