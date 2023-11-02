using System;
using System.Data.Common;
using System.Linq;
using Cfg = Standard.ToList.Model.Aggregates.Configuration;

namespace Standard.ToList.Model.ViewModels.Configuration
{
	public class ConfigurationViewModel
	{
        public ConfigurationViewModel(Cfg.Configuration configuration, ConfigurationRequest request = null)
        {
            Id = configuration.Id;
            Name = configuration.Name;
            Workers = configuration.Workers
                                   .Where(it => request.WorkerTypes == null || request.WorkerTypes.Contains(it.Type))
                                   .Select(it => new WorkerViewModel(it))
                                   .ToArray();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public WorkerViewModel[] Workers { get; set; }
    }
}

