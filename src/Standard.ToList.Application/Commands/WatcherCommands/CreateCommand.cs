﻿using System;
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

        public Expression<Func<Watcher, bool>> Expression => it => it.ProductId == ProductId &&
                                                                   (it.UserId == UserId || RoleType == RoleType.Admin);
    }
}
