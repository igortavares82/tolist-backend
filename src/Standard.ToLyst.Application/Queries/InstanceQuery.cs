using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Standard.ToLyst.Application.Extensions;
using Standard.ToLyst.Model.Aggregates.Lists;
using Standard.ToLyst.Model.Aggregates.Lysts;
using Standard.ToLyst.Model.Aggregates.Markets;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.Constants;
using Standard.ToLyst.Model.ViewModels.Lysts;

namespace Standard.ToLyst.Application.Queries
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
            var lyst = await _lystRepository.GetOneAsync(request.Query());

            if (lyst == null)
                return result.SetResult(ResultStatus.NotFound, Messages.NotFound.SetMessageValues("Lyst"));

            var markets = _marketRepository.GetAsync(it => it.IsEnabled == true).Result.ToArray();
            var instances = lyst.Instances
                                //.Where(it => request.Expression(it))
                                .Select(it => new InstanceViewModel(it, markets));

            return result.SetResult(instances, ResultStatus.Success);
        }
    }
}

