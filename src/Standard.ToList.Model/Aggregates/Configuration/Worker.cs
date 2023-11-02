using System;
using MongoDB.Bson;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Model.Aggregates.Configuration
{
	public class Worker : Entity
	{
        public Worker(WorkerType type,
                      int delay,
                      Page page,
                      string properties)
        {
            Id = ObjectId.GenerateNewId().ToString();
            CreateDate = DateTime.UtcNow;
            Type = type;
            Delay = delay;
            Page = page ?? new Page(10, 0, 0);
            Properties = properties;
        }

        public WorkerType Type { get; set; }
		public int Delay { get; set; }
		public Page Page { get; set; }
        public string Properties { get; set; }

        public void Start()
        {
            if (Page.Pages != Page.Index)
                return;

            Page.Count = 0;
            Page.Index = 0;
            LastUpdate = DateTime.UtcNow;
        }

        public void End()
        {
            if (Page.Pages == Page.Index || Page.Count == 0)
            {
                Page.Count = 0;
                Page.Index = 0;
                return;
            }

            Page.Index++;
            LastUpdate = DateTime.UtcNow;
        }
	}
}

