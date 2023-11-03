using System;
using Standard.ToList.Model.SeedWork;

namespace Standard.ToList.Model.Aggregates.Configuration
{
    public class Configuration : Entity, IAggregateRoot
    {
        public Configuration(string name, Worker[] workers)
        {
            Name = name;
            Workers = workers;
        }

        public string Name { get; set; }
		public Worker[] Workers { get; set; }
        public Logger Logger { get; set; }

        public void Update(string name, Worker[] workers)
        {
            Name = name;
            Workers = workers;
            LastUpdate = DateTime.UtcNow;
        }
	}
}
