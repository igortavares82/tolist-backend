using Standard.ToList.Application.Services;
using Standard.ToList.Model.Aggregates.Configuration;
using Standard.ToList.Model.Aggregates.Markets;

namespace Standard.ToList.Api.Workers
{
    public class ProductSearchWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMarketService _marketService;
        private readonly WorkerService _workerService;

        public ProductSearchWorker(IServiceProvider serviceProvider)
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
                await _workerService.ExecuteAsync(WorkerType.ProductSearchMissing,
                                                  (worker) => _marketService.SearchMissingProductsAsync(worker),
                                                  stoppingToken);
            }
        }
    }
}

