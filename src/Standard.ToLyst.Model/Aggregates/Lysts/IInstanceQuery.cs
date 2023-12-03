using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.ViewModels.Lysts;

namespace Standard.ToLyst.Model.Aggregates.Lysts
{
    public interface IInstanceQuery
    {
		Task<Result<IEnumerable<InstanceViewModel>>> GetAsync(InstanceRequest request);
	}
}
