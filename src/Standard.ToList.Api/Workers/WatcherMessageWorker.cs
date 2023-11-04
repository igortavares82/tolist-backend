using Standard.ToList.Application.Services;
using Standard.ToList.Model.Aggregates.Configuration;
using Standard.ToList.Model.Aggregates.Watchers;

namespace Standard.ToList.Api.Workers
{
    public class WatcherMessageWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IWatcherService _watcherService;
        private readonly WorkerService _workerService;
        private readonly ILogger<WatcherMessageWorker> _logger;

        public WatcherMessageWorker(IServiceProvider serviceProvider)
		{
            _serviceProvider = serviceProvider;

            var scope = _serviceProvider.CreateScope();
            _workerService = scope.ServiceProvider.GetService<WorkerService>();
            _watcherService = scope.ServiceProvider.GetService<IWatcherService>();
            _logger = scope.ServiceProvider.GetService<ILogger<WatcherMessageWorker>>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                _logger.LogInformation("Starting watcher message worker service.");

                await _workerService.ExecuteAsync(WorkerType.WatcherSendMessage,
                                                  (worker) => _watcherService.SendMessagesAsync(worker),
                                                  stoppingToken);

                _logger.LogInformation("Ending watcher message worker service.");                                  
            }
        }
    }
}

