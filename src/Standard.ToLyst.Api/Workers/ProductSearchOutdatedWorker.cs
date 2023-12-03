using System;
using Standard.ToLyst.Application.Services;
using Standard.ToLyst.Model.Aggregates.Configuration;
using Standard.ToLyst.Model.Aggregates.Markets;
using Standard.ToLyst.Model.Aggregates.Watchers;

namespace Standard.ToLyst.Api.Workers
{
	public class ProductSearchOutdatedWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMarketService _marketService;
        private readonly WorkerService _workerService;
        private readonly ILogger<ProductSearchOutdatedWorker> _logger;

        public ProductSearchOutdatedWorker(IServiceProvider serviceProvider)
		{
            _serviceProvider = serviceProvider;

            var scope = _serviceProvider.CreateScope();
            _workerService = scope.ServiceProvider.GetService<WorkerService>();
            _marketService = scope.ServiceProvider.GetService<IMarketService>();
            _logger = scope.ServiceProvider.GetService<ILogger<ProductSearchOutdatedWorker>>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Starting product search outdated worker service.");

                await _workerService.ExecuteAsync(WorkerType.ProductSearchOutdated,
                                                  (worker) => _marketService.SearchOutdatedProductsAsync(worker),
                                                  stoppingToken);

                _logger.LogInformation("Ending product search outdated worker service.");
            }
        }
    }
}

