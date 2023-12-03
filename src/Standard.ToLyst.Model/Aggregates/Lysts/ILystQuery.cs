using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.ViewModels.Lysts;

namespace Standard.ToLyst.Model.Aggregates.Lists
{
    public interface ILystQuery
	{
        Task<Result<IEnumerable<LystViewModel>>> GetAsync(Request request);
        Task<Result<LystViewModel>> GetAsync(string id);
        Task<Result<IEnumerable<LystViewModel>>> GetAsync(LystRequest request);
    }
}
