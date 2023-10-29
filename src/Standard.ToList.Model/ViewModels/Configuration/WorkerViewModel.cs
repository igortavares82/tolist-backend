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
            Delay = worker.Delay;
            Index = worker.Page.Index;
            Limit = worker.Page.Limit;
            Properties = Properties;
        }

        public string Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public WorkerType Type { get; set; }
        public int Delay { get; set; }
        public int Items { get; set; }
        public int Index { get; set; }
        public int Limit { get; set; }
        public string Properties { get; set; }
    }
}

