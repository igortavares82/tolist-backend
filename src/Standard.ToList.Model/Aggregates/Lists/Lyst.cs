using System;
using System.Collections.Generic;
using System.Linq;

namespace Standard.ToList.Model.Aggregates.Lists
{
	public class Lyst : Entity
	{
		public string Name { get; set; }
		public string UserId { get; set; }
		public bool IsDraft { get; set; }
		private List<Item> Items { get; set; }

		public Lyst(string name, string userId, bool isDraft, List<Item> items) : base()
		{
			Name = name;
			UserId = userId;
			IsDraft = isDraft;
			Items = items;
		}

		public void SetItems(params Item[] items)
		{
            Items = Items ?? new List<Item>();
			Items.AddRange(items);
		}

		public IEnumerable<Item> GetItems() => Items.AsEnumerable();
	}
}

