using Standard.ToLyst.Application.Services;
using Standard.ToLyst.Model.Aggregates.Configuration;
using Standard.ToLyst.Model.Aggregates.Markets;

namespace Standard.ToLyst.Api.Workers
{
    public class ProductSearchWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMarketService _marketService;
        private readonly WorkerService _workerService;
        private readonly ILogger<ProductSearchWorker> _logger;

        public ProductSearchWorker(IServiceProvider serviceProvider)
		{
            _serviceProvider = serviceProvider;

            var scope = _serviceProvider.CreateScope();
            _workerService = scope.ServiceProvider.GetService<WorkerService>();
            _marketService = scope.ServiceProvider.GetService<IMarketService>();
            _logger = scope.ServiceProvider.GetService<ILogger<ProductSearchWorker>>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                 _logger.LogInformation("Starting product search worker service.");

                await _workerService.ExecuteAsync(WorkerType.ProductSearchMissing,
                                                  (worker) => _marketService.SearchMissingProductsAsync(worker),
                                                  stoppingToken);

                _logger.LogInformation("Ending product search worker service.");
            }
        }
    }
}

