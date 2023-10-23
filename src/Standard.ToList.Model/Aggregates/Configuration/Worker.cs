using System;
using MongoDB.Bson;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Model.Aggregates.Configuration
{
	public class Worker : Entity
	{
        public Worker(WorkerType type,
                      int interval,
                      string properties)
        {
            Id = ObjectId.GenerateNewId().ToString();
            CreateDate = DateTime.UtcNow;
            Type = type;
            Interval = interval;
            Properties = properties;
        }

        public WorkerType Type { get; set; }
		public int Interval { get; set; }
		public Page Page { get; set; }
        public string Properties { get; set; }

        public void Start(Page page) => Page = page;

        public void Update()
        {
            if (Page.Count == Page.Index)
                return;

            ++Page.Index;
            LastUpdate = DateTime.UtcNow;
        }
	}
}

