using System;
using System.Linq.Expressions;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Aggregates.Users;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application.Extensions
{
    public static class RequestExtension
	{
		public static Expression<Func<Lyst, bool>> Query(this Request request)
		{
            Expression<Func<Lyst, bool>> expression = lyst => request.ResourceId == lyst.Id &&
															  (request.UserId == lyst.UserId || request.RoleType == RoleType.Admin);

            return expression;
		}
    }
}

