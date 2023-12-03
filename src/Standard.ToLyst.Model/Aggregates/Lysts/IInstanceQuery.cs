using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.ViewModels.Lysts;

namespace Standard.ToList.Model.Aggregates.Lysts
{
    public interface IInstanceQuery
    {
		Task<Result<IEnumerable<InstanceViewModel>>> GetAsync(InstanceRequest request);
	}
}
