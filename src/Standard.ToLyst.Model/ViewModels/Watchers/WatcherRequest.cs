using System;
using System.Linq.Expressions;
using Standard.ToLyst.Model.Aggregates.Users;
using Standard.ToLyst.Model.Aggregates.Watchers;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Model.ViewModels.Watchers
{
    public class WatcherRequest : Request
	{
		public string Name { get; set; }
		public DateTime? Start { get; set; }
		public DateTime? End { get; set; }

        public Expression<Func<Watcher, bool>> Expression => it => (it.UserId == UserId || RoleType == RoleType.Admin) &&
																   (string.IsNullOrEmpty(Name) || it.Name.ToLower().Contains(Name.ToLower())) &&
																   (!Start.HasValue || !End.HasValue || (it.CreateDate >= Start && it.CreateDate <= End));
    }
}

