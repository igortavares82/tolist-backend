using System;
using System.Linq.Expressions;
using Standard.ToLyst.Model.Aggregates.Lists;
using Standard.ToLyst.Model.Aggregates.Users;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Application.Extensions
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

