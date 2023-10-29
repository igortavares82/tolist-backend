using System;
using System.Linq.Expressions;
using Standard.ToList.Model.Aggregates;
using Standard.ToList.Model.Aggregates.Users;

namespace Standard.ToList.Model.Common
{
	public class Request
	{
		public string ResourceId { get; set; }
		public string UserId { get; set; }
		public RoleType RoleType { get; set; }
		public Page Page { get; set; } = new Page();
		public Order Order { get; set; }

		public bool IsOperationAllowed()
		{
			return ResourceId == UserId || RoleType == RoleType.Admin;
		}

        public Expression<Func<Entity, bool>> Expression => it => it.Id == ResourceId;

    }
	
	public class Page
	{
		public Page() { }

		public Page(int limit, int count, int index = 1)
		{
			Limit = limit;
			Count = count;
			Index = index;
		}

		private int size = 1;
        public int Limit
		{
			get { return size; }
			set
			{
				if (value == 0)
					size = 1;
				else
					size = value;
			}
		}

		public int Index { get; set; }
        public int Skip => Limit * (Index - 1);
        public int Count { get; set; }
		public int Pages => (int) Math.Ceiling((decimal) Count / Limit);


    }

	public class Order
	{
		public string Field { get; set; }
		public int Direction { get; set; } 
	}
}
