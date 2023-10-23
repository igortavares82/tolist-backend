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

		public Page(uint size, uint count, uint index = 1)
		{
			Size = size;
			Count = count;
			Index = index;
		}

		private uint size = 1;
        public uint Size
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

		public uint Index { get; set; }
        public uint Skip => Size * (Index - 1);
        public uint Count { get; set; }
		public uint Pages => (uint) Math.Ceiling((decimal) Count / Size);


    }

	public class Order
	{
		public string Field { get; set; }
		public int Direction { get; set; } 
	}
}
