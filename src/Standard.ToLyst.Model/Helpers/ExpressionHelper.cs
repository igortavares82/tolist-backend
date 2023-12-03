using System;
using System.Linq.Expressions;
using Standard.ToLyst.Model.Aggregates.Lists;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Model.Helpers
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
