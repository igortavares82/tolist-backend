using System;
using Microsoft.Extensions.Options;
using Standard.ToList.Model.Aggregates.Configuration;
using Standard.ToList.Model.Options;

namespace Standard.ToList.Infrastructure.Repositories
{
	public class ConfigurationRepository : Repository<Configuration>, IConfigurationRepository
	{
		public ConfigurationRepository(IOptions<AppSettingOptions> settings) : base(settings)
        {
		}


	}
}

