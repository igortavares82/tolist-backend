using System.Threading.Tasks;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.ViewModels.Lysts;

namespace Standard.ToList.Application.Queries
{
    public class LystQuery : ILystQuery
    {
        private readonly ILystRepository _repository;

		public LystQuery(ILystRepository repository)
		{
            _repository = repository;

        }

        public async Task<Result<LystViewModel>> GetAsync(string id)
        {
            var result = await _repository.GetAsync(id);
            return new Result<LystViewModel>(new LystViewModel(result), result == null ? ResultStatus.NotFound : ResultStatus.Success);
        }
    }
}

