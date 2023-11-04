using System;
using Standard.ToList.Model.Aggregates.Configuration;

namespace Standard.ToList.Model.ViewModels.Configuration
{
    public class WorkerViewModel : ViewModel
	{
        public WorkerViewModel(Worker worker) : base(worker)
        {
            Type = worker.Type;
            Delay = worker.Delay;
            Index = worker.Page.Index;
            Limit = worker.Page.Limit;
            Count = worker.Page.Count;
            Pages = worker.Page.Pages;
            Properties = Properties;
        }
        
        public WorkerType? Type { get; set; }
        public string TypeName => Type.ToString();
        public int Delay { get; set; }
        public int Count { get; set; }
        public int Index { get; set; }
        public int Limit { get; set; }
        public int Pages { get; set; }
        public string Properties { get; set; }
    }
}

