using System;
using System.Linq.Expressions;
using MediatR;
using Standard.ToLyst.Model.Aggregates.Users;
using Standard.ToLyst.Model.Aggregates.Watchers;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.ViewModels.Watchers;

namespace Standard.ToLyst.Application.Commands.WatcherCommands
{
    public class CreateCommand : Request, IRequest<Result<WatcherViewModel>>
    {
        public string ProductId { get; set; }
        public decimal Current { get; set; }
        public decimal Desired { get; set; }

        public Expression<Func<Watcher, bool>> Expression => it => it.ProductId == ProductId &&
                                                                   (it.UserId == UserId || RoleType == RoleType.Admin);
    }
}
