using System;
using Microsoft.Extensions.Options;
using Standard.ToLyst.Model.Aggregates.Configuration;
using Standard.ToLyst.Model.Options;

namespace Standard.ToLyst.Infrastructure.Repositories
{
	public class ConfigurationRepository : Repository<Configuration>, IConfigurationRepository
	{
		public ConfigurationRepository(IOptions<AppSettingOptions> settings) : base(settings)
        {
		}


	}
}

