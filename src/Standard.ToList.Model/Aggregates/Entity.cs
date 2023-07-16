using System;

namespace Standard.ToList.Model.Aggregates
{
	public class Entity
	{
		public string Id { get; set; }
		public bool? IsEnabled { get; set; }
		public DateTime? CreateDate { get; set; }
		public DateTime? LastUpdate { get; set; }

		public Entity()
		{
			IsEnabled = true;
			CreateDate = DateTime.Now;
		}
	}
}

