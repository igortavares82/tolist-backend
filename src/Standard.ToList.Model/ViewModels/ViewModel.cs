﻿using System;
using Standard.ToList.Model.Aggregates;

namespace Standard.ToList.Model
{
    public class ViewModel
    {
        public ViewModel()
        {
            
        }

        public ViewModel(Entity entity)
        {
            Id = entity.Id;
            IsEnabled = entity.IsEnabled;
            CreateDate = entity.CreateDate;
            LastUpdate = entity.LastUpdate;
            ExpireAt = entity.ExpireAt;
        }

        public string Id { get; set; }
		public bool? IsEnabled { get; set; }
		public DateTime? CreateDate { get; set; }
		public DateTime? LastUpdate { get; set; }
		public DateTime? ExpireAt { get; set; }
    }
}
