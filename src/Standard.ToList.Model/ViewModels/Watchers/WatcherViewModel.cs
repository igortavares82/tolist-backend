using System;
using Standard.ToList.Model.Aggregates.Watchers;

namespace Standard.ToList.Model.ViewModels.Watchers
{
    public class WatcherViewModel
	{
        public WatcherViewModel(Watcher watcher)
        {
            Id = watcher.Id;
            Name = watcher.Name;
            ProductId = watcher.ProductId;
            Price = watcher.Price;
            Current = watcher.Current;
            Desired = watcher.Desired;
            CreateDate = watcher.CreateDate;
            LastUpdate = watcher.LastUpdate;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal Current { get; set; }
        public decimal Desired { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}

