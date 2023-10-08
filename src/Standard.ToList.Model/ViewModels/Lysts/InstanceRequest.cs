using System;
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

        public override Func<TEntity, bool> ToDelegate<TEntity>()
        {
            Func<Instance, bool> expression = instance =>  (string.IsNullOrEmpty(Name) || instance.Name.ToLower().Contains(Name.ToLower())) &&
                                                           (!Start.HasValue || !End.HasValue || instance.CreateDate >= Start && instance.CreateDate <= End);

            return (Func<TEntity, bool>) expression;
        }
    }
}
