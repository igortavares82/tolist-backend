using Standard.ToList.Application.Services;
using Standard.ToList.Model.Aggregates.Configuration;
using Standard.ToList.Model.Aggregates.Watchers;

namespace Standard.ToList.Api.Workers
{
    public class WatcherUpdateWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IWatcherService _watcherService;
        private readonly WorkerService _workerService;
        private readonly ILogger<WatcherUpdateWorker> _logger;

        public WatcherUpdateWorker(IServiceProvider serviceProvider)
		{
            _serviceProvider = serviceProvider;

            var scope = _serviceProvider.CreateScope();
            _workerService = scope.ServiceProvider.GetService<WorkerService>();
            _watcherService = scope.ServiceProvider.GetService<IWatcherService>();
            _logger = scope.ServiceProvider.GetService<ILogger<WatcherUpdateWorker>>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Starting watcher update service.");

                await _workerService.ExecuteAsync(WorkerType.WatcherUpdate,
                                                  (worker) => _watcherService.UpdateWatchersAsync(worker),
                                                  stoppingToken);

                _logger.LogInformation("Ending watcher update service.");
            }
        }
    }
}

