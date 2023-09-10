using System;
using System.Linq;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.ViewModels.Products;

namespace Standard.ToList.Model.ViewModels.Lysts
{
	public class LystViewModel
	{
		public string Id { get; set; }
		public bool? IsEnabled { get; set; }
		public DateTime? CreateDate { get; set; }
		public DateTime? LastUpdate { get; set; }
		public string Name { get; set; }
		public string UserId { get; set; }
		public bool IsDraft { get; set; }
		public ProductViewModel[] Products { get; set; }

        public LystViewModel(Lyst lyst)
        {
            if (lyst == null)
                return;

            Id = lyst.Id;
            IsEnabled = lyst.IsEnabled;
            CreateDate = lyst.CreateDate;
            LastUpdate = lyst.LastUpdate;
            Name = lyst.Name;
            UserId = lyst.UserId;
            IsDraft = lyst.IsDraft;
        }

        public LystViewModel(Lyst lyst, Market[] markets) : this(lyst)
		{
			if (lyst == null)
				return;

			Products = lyst.Products.Select(it => new ProductViewModel(it, markets.First(_it => _it.Id == it.Market.Id))).ToArray();
		}
	}
}

