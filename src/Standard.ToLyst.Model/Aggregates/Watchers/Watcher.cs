﻿using System;
using Standard.ToLyst.Model.Aggregates;
using Standard.ToLyst.Model.SeedWork;

namespace Standard.ToLyst.Model.Aggregates.Watchers
{
    public class Watcher : Entity, IAggregateRoot
    {
        public Watcher(string userId,
                       string name,
                       string productId,
                       decimal price,
                       decimal current,
                       decimal desired)
        {
            UserId = userId;
            Name = name;
            ProductId = productId;
            Price = price;
            Current = current;
            Desired = desired;
        }

        public string UserId { get; set; }
		public string Name { get; set; }
		public string ProductId { get; set; }
		public decimal Price { get; set; }
        public decimal Current { get; set; }
        public decimal Desired { get; set; }
        public DateTime? LastSentMessageDate { get; set; }

        public void Update(string name,
                           decimal price,
                           decimal current,
                           decimal desired)
        {
            Name = name;
            Price = price;
            Current = current;
            Desired = desired;
        }

        public void Update(string name,
                           decimal desired)
        {
            Name = name ?? Name;
            Desired = desired;
            LastUpdate = DateTime.UtcNow;
        }

        public void Update(decimal current) => Current = current;

        public bool IsDesired() => Current <= Desired;

        public bool IsLower() => Current <= Price;
    }
}
