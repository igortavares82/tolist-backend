using System;
using System.Linq.Expressions;
using Standard.ToList.Model.Aggregates.Users;
using Standard.ToList.Model.Aggregates.Watchers;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Model.ViewModels.Watchers
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

