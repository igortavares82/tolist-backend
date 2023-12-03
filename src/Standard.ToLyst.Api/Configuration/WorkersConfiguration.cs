using System;
using Standard.ToLyst.Api.Workers;

namespace Standard.ToLyst.Api.Configuration
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

