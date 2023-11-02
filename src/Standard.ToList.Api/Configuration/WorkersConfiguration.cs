using System;
using Standard.ToList.Api.Workers;

namespace Standard.ToList.Api.Configuration
{
	public static class WorkersConfiguration
	{
		public static void ConfigureWorker(this IServiceCollection services)
		{
			services.AddHostedService<ProductSearchWorker>();
			services.AddHostedService<ProductSearchOutdatedWorker>();
            services.AddHostedService<WatcherMessageWorker>();
            services.AddHostedService<WatcherUpdateWorker>();
        }
	}
}

