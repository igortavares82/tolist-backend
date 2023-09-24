using System;
using System.Collections.Generic;
using System.Linq;
using Standard.ToList.Model.Aggregates.Lysts;
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
        public List<Instance> Instances { get; set; }

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

        public void Update(string name, bool isDraft, bool isEnabled, Product[] items)
        {
            Name = name;
            IsDraft = isDraft;
            IsEnabled = isEnabled;
            LastUpdate = DateTime.Now;
            Products.Clear();
            Products.AddRange(items);
        }

        public Instance CreateInstance(string name)
        {
            name = name ?? $"{Name} - Copy - {DateTime.Now}";
            var instance = new Instance(name, new List<Product>(Products));
            Instances = Instances ?? new List<Instance>();
            Instances.Add(instance);

            return instance;
        }

        public void DeleteInstance(string id)
        {
            var instance = Instances.FirstOrDefault(it => it.Id == id);
            Instances.Remove(instance);
            LastUpdate = DateTime.UtcNow;
        }
	}
}

