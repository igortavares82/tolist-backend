using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using Standard.ToList.Model.Aggregates.Products;

namespace Standard.ToList.Model.Aggregates.Lysts
{
    public class Instance : Entity
	{
        public Instance(string name, List<Product> products)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Name = name;
            Products = products;
            CreateDate = DateTime.Now;
        }

        public string Name { get; set; }
        public List<Product> Products { get; set; }

        public void CheckProduct(string productId, bool value)
        {
            Products.Where(it => it.Id == productId)
                    .ToList()
                    .ForEach(it =>
                    {
                        it.SetEnabled(value);
                    });
        }

        public void Update(string name, bool? isEnabled)
        {
            Name = name ?? Name;
            IsEnabled = isEnabled ?? isEnabled;
            LastUpdate = DateTime.UtcNow;
        }
    }
}

