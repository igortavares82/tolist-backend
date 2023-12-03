using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.ViewModels.Watchers;

namespace Standard.ToLyst.Model.Aggregates.Watchers
{
    public interface IWatcherQuery
	{
		Task<Result<WatcherViewModel>> GetAsync(Request request);
        Task<Result<IEnumerable<WatcherViewModel>>> GetAsync(WatcherRequest request);
    }
}

