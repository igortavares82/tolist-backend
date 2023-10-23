using System;
using System.Linq;
using Cfg = Standard.ToList.Model.Aggregates.Configuration;

namespace Standard.ToList.Model.ViewModels.Configuration
{
	public class ConfigurationViewModel
	{
        public ConfigurationViewModel(Cfg.Configuration configuration)
        {
            Name = configuration.Name;
            Workers = configuration.Workers.Select(it => new WorkerViewModel(it)).ToArray();

        }

        public string Name { get; set; }
        public WorkerViewModel[] Workers { get; set; }
    }
}

