using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Standard.ToList.Model.Aggregates.Lysts;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Model.ViewModels.Lysts
{
    public class InstanceRequest : Request
	{
		[FromRoute] public string InstanceId { get; set; }
		[FromQuery] public string Name { get; set; }
		[FromQuery] public DateTime? Start { get; set; }
        [FromQuery] public DateTime? End { get; set; }

        public Expression<Func<Instance, bool>> Expression => it => (string.IsNullOrEmpty(Name) || it.Name.ToLower().Contains(Name.ToLower())) &&
                                                                    (!Start.HasValue || !End.HasValue || it.CreateDate >= Start && it.CreateDate <= End);
    }
}
