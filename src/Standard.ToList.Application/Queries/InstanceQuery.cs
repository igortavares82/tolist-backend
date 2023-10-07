using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Standard.ToList.Application.Extensions;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Aggregates.Lysts;
using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.Constants;
using Standard.ToList.Model.ViewModels.Lysts;

namespace Standard.ToList.Application.Queries
{
    public class InstanceQuery : IInstanceQuery
    {
        private readonly ILystRepository _lystRepository;
        private readonly IMarketRepository _marketRepository;

        public InstanceQuery(ILystRepository lystRepository, IMarketRepository marketRepository)
        {
            _lystRepository = lystRepository;
            _marketRepository = marketRepository;
        }

        public async Task<Result<IEnumerable<InstanceViewModel>>> GetAsync(InstanceRequest request)
        {
            var result = new Result<IEnumerable<InstanceViewModel>>(null);
            var lyst = await _lystRepository.GetOneAsync(it => it.Id == request.ResourceId &&
                                                               it.UserId == request.UserId);

            if (lyst == null)
                return result.SetResult(ResultStatus.NotFound, Messages.NotFound.SetMessageValues("Lyst"));

            var markets = _marketRepository.GetAsync(it => it.IsEnabled == true).Result.ToArray();
            var instances = lyst.Instances
                                .Where(request.ToExpression<Instance>())
                                .Select(it => new InstanceViewModel(it, markets));

            return result.SetResult(instances, ResultStatus.Success);
        }
    }
}

