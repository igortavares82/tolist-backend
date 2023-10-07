using System.Threading.Tasks;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.ViewModels.Lysts;
using System.Linq;
using System.Collections.Generic;

namespace Standard.ToList.Application.Queries
{
    public class LystQuery : ILystQuery
    {
        private readonly ILystRepository _lystRepository;
        private readonly IMarketRepository _marketRepository;


        public LystQuery(ILystRepository repository, IMarketRepository marketRepository)
		{
            _lystRepository = repository;
            _marketRepository = marketRepository;
        }

        public async Task<Result<LystViewModel>> GetAsync(string id)
        {
            var lystResult = await _lystRepository.GetOneAsync(id);
            var marketResult = await _marketRepository.GetAsync(it => it.IsEnabled == true);

            return new Result<LystViewModel>(new LystViewModel(lystResult, marketResult.ToArray()),
                                            lystResult == null ? ResultStatus.NotFound : ResultStatus.Success);
        }

        public async Task<Result<IEnumerable<LystViewModel>>> GetAsync(Request request)
        {
            var result = await _lystRepository.GetAsync(it => it.UserId == request.UserId);

            return new Result<IEnumerable<LystViewModel>>(result.Select(it => new LystViewModel(it)).AsEnumerable(),
                                                          ResultStatus.Success);
        }

        public async Task<Result<IEnumerable<LystViewModel>>> GetAsync(LystRequest request)
        {
            var result = await _lystRepository.GetAsync(request.UserId, request.Name, request.IsDraft, request.IsEnabled, request.Page);

            return new Result<IEnumerable<LystViewModel>>(result.Select(it => new LystViewModel(it)).AsEnumerable(),
                                                          ResultStatus.Success);
        }
    }
}

