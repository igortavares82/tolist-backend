using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Standard.ToLyst.Application.Extensions;
using Standard.ToLyst.Model.Aggregates.Users;
using Standard.ToLyst.Model.Aggregates.Watchers;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.Constants;
using Standard.ToLyst.Model.ViewModels.Watchers;

namespace Standard.ToLyst.Application.Queries
{
	public class WatcherQuery : IWatcherQuery
    {
        private readonly IWatcherRepository _repository;

        public WatcherQuery(IWatcherRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<WatcherViewModel>> GetAsync(Request request)
        {
            var result = new Result<WatcherViewModel>(null);
            var watcher = await _repository.GetOneAsync(it => it.Id == request.ResourceId &&
                                                              (it.UserId == request.UserId || request.RoleType == RoleType.Admin));

            if (watcher == null)
                return result.SetResult(ResultStatus.NotFound, Messages.NotFound.SetMessageValues("Watcher"));

            return result.SetResult(new WatcherViewModel(watcher), ResultStatus.Success);
        }

        public async Task<Result<IEnumerable<WatcherViewModel>>> GetAsync(WatcherRequest request)
        {
            var result = new Result<IEnumerable<WatcherViewModel>>(null);
            var watchers = await _repository.GetAsync(request.Expression);

            return result.SetResult(watchers.Select(it => new WatcherViewModel(it)).AsEnumerable(), ResultStatus.Success);
        }
    }
}

