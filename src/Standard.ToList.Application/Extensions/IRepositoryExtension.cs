using System;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Common;
using System.Linq.Expressions;
using Standard.ToList.Model.Aggregates;

namespace Standard.ToList.Application.Extensions
{
	public static class IRepositoryExtension
	{
		public static Expression<Func<Lyst, bool>> Default(this IRepository<Lyst> input, Request request)
        {
            return (Lyst lyst) => lyst.Id == request.ResourceId &&
                                  lyst.UserId == request.UserId;
        }

    }
}

