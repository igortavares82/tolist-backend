using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.ViewModels.Lysts;

namespace Standard.ToList.Model.Aggregates.Lists
{
    public interface ILystQuery
	{
        Task<Result<IEnumerable<LystViewModel>>> GetAsync(Request request);
        Task<Result<LystViewModel>> GetAsync(string id);
        Task<Result<IEnumerable<LystViewModel>>> GetAsync(LystRequest request);
    }
}
