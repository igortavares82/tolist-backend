using System;
using Standard.ToList.Model.Aggregates.Logs;
using Standard.ToList.Model.SeedWork;

namespace Standard.ToList.Model.Aggregates.Configuration
{
    public class Configuration : Entity, IAggregateRoot
    {
        public Configuration(string name, Worker[] workers, Logger logger)
        {
            Name = name;
            Workers = workers;
            Logger = logger;
        }

        public string Name { get; set; }
		public Worker[] Workers { get; set; }
        public Logger Logger { get; set; }

        public void Update(string name, Worker[] workers, Logger logger)
        {
            Name = name;
            Workers = workers;
            Logger = logger;
            LastUpdate = DateTime.UtcNow;
        }
	}
}
