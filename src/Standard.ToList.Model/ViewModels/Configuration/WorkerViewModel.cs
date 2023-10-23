using System;
using Standard.ToList.Model.Aggregates.Configuration;

namespace Standard.ToList.Model.ViewModels.Configuration
{
    public class WorkerViewModel
	{
        public WorkerViewModel(Worker worker)
        {
            Id = worker.Id;
            CreateDate = worker.CreateDate;
            LastUpdate = worker.LastUpdate;
            Type = worker.Type;
            Interval = worker.Interval;
            Items = worker.Size;
            Next = worker.Next;
            Properties = Properties;
        }

        public string Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public WorkerType Type { get; set; }
        public int Interval { get; set; }
        public int Items { get; set; }
        public int Next { get; set; }
        public string Properties { get; set; }
    }
}

