using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Standard.ToList.Application.Extensions;
using Standard.ToList.Model.Aggregates.Users;
using Standard.ToList.Model.Aggregates.Watchers;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.Constants;
using Standard.ToList.Model.ViewModels.Watchers;

namespace Standard.ToList.Application.Queries
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

