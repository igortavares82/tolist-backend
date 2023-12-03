using System;
using Standard.ToLyst.Model.Aggregates.Logs;
using Standard.ToLyst.Model.SeedWork;

namespace Standard.ToLyst.Model.Aggregates.Configuration
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
