using System;
using System.Linq.Expressions;
using MediatR;
using Standard.ToList.Model.Aggregates.Users;
using Standard.ToList.Model.Aggregates.Watchers;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application.Commands.WatcherCommands
{
    public class CreateCommand : Request, IRequest<Result<Watcher>>
    {
        public string ProductId { get; set; }
        public decimal Current { get; set; }
        public decimal Desired { get; set; }

        public override Func<TEntity, bool> ToDelegate<TEntity>()
        {
            Func<Watcher, bool> expression = watcher => watcher.ProductId == ProductId && (watcher.UserId == UserId || RoleType == RoleType.Admin);
            return (Func<TEntity, bool>) expression;
        }

        public override Expression<Func<TEntity, bool>> ToExpression<TEntity>(TEntity entity)
        {
            return entity => ToDelegate<TEntity>()(entity);
        }
    }
}
