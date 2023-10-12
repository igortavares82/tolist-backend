using System;
using System.Linq.Expressions;
using MediatR;
using Standard.ToList.Model.Aggregates.Users;
using Standard.ToList.Model.Aggregates.Watchers;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application.Commands.WatcherCommands
{
    public class UpdateCommand : Request, IRequest<Result<Unit>>
    {
        public string Name { get; set; }
        public decimal Desired { get; set; }

        public new Expression<Func<Watcher, bool>> Expression => it => it.Id == ResourceId &&
                                                                       (it.UserId == UserId || RoleType == RoleType.Admin);
    }
}
