using System;
using System.Linq;
using System.Linq.Expressions;

namespace Standard.ToLyst.Model.Extensions
{
    public static class ExpressionExtension
    {
        public static Expression<Func<TEntity, bool>> BuildExpression<TEntity>(this Expression<Func<TEntity, bool>>[] expressions)
        {
            var exp = expressions.First().Body;

            for (int i = 1; expressions.Length > 1 && i < expressions.Length; i++)
                exp = Expression.And(exp, expressions[i].Body);

            var param = expressions.SelectMany(it => it.Parameters).First();
            return Expression.Lambda<Func<TEntity, bool>>(exp, param);
        }

    }
}