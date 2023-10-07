using System;
using System.Linq.Expressions;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Model.Helpers
{
    public static class ExpressionHelper
	{
		public static Expression<Func<Lyst, bool>> Default(this Lyst input, Request request)
        {
			return (Lyst lyst) => lyst.Id == request.ResourceId &&
								  lyst.UserId == request.UserId;
		}
	}
}
