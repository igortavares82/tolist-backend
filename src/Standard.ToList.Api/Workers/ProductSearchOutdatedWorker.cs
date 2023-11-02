using System;
using Standard.ToList.Application.Services;
using Standard.ToList.Model.Aggregates.Configuration;
using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.Aggregates.Watchers;

namespace Standard.ToList.Api.Workers
{
	public class ProductSearchOutdatedWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMarketService _marketService;
        private readonly WorkerService _workerService;

        public ProductSearchOutdatedWorker(IServiceProvider serviceProvider)
		{
            _serviceProvider = serviceProvider;

            var scope = _serviceProvider.CreateScope();
            _workerService = scope.ServiceProvider.GetService<WorkerService>();
            _marketService = scope.ServiceProvider.GetService<IMarketService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _workerService.ExecuteAsync(WorkerType.ProductSearchOutdated,
                                                  (worker) => _marketService.SearchOutdatedProductsAsync(worker),
                                                  stoppingToken);
            }
        }
    }
}

