using System;
using System.Collections.Generic;
using System.Linq;
using Standard.ToList.Model.SeedWork;

namespace Standard.ToList.Model.Aggregates.Lists
{
	public class Lyst : Entity, IAggregateRoot
    {
		public string Name { get; set; }
		public string UserId { get; set; }
		public bool IsDraft { get; set; }
		private List<string> Items { get; set; }

        public Lyst(string name, string userId, bool isDraft, string[] items) : base()
		{
			Name = name;
			UserId = userId;
			IsDraft = isDraft;
			Items = items.ToList();
		}

		public void SetItems(params string[] items)
		{
            Items = Items ?? new List<string>();
			Items.AddRange(items);
		}

		public IEnumerable<string> GetItems() => Items.AsEnumerable();
	}
}

