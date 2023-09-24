using System;
using System.Linq;
using Standard.ToList.Model.Aggregates.Lysts;
using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.ViewModels.Products;

namespace Standard.ToList.Model.ViewModels.Lysts
{
    public class InstanceViewModel
	{
        public string Id { get; set; }
        public bool? IsEnabled { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string Name { get; set; }
        public ProductViewModel[] Products { get; set; }

        public InstanceViewModel(Instance instance, Market[] markets)
		{
            if (instance == null)
                return;

            Id = instance.Id;
            IsEnabled = instance.IsEnabled;
            CreateDate = instance.CreateDate;
            LastUpdate = instance.LastUpdate;
            Name = instance.Name;

            Products = instance.Products.Select(it => new ProductViewModel(it, markets.First(_it => _it.Id == it.Market.Id))).ToArray();
        }
	}
}

