using System.Collections.Generic;
using System.Linq;
using Standard.ToList.Model.Aggregates.Products;
using Standard.ToList.Model.SeedWork;

namespace Standard.ToList.Model.Aggregates.Lists
{
    public class Lyst : Entity, IAggregateRoot
    {
		public string Name { get; set; }
		public string UserId { get; set; }
		public bool IsDraft { get; set; }

        public List<Product> Products { get; set; }

        public Lyst(string name, string userId, bool isDraft, Product[] items) : base()
		{
			Name = name;
			UserId = userId;
			IsDraft = isDraft;
			Products = items.ToList();
		}

        public void SetItems(params Product[] items)
        {
            Products = Products ?? new List<Product>();
            Products.AddRange(items);
        }

        public IEnumerable<Product> GetItems() => Products.AsEnumerable();
	}
}

