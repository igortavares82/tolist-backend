﻿using Standard.ToList.Application.Services;
using Standard.ToList.Model.Aggregates.Configuration;
using Standard.ToList.Model.Aggregates.Watchers;

namespace Standard.ToList.Api.Workers
{
    public class WatcherMessageWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IWatcherService _watcherService;
        private readonly WorkerService _workerService;

        public WatcherMessageWorker(IServiceProvider serviceProvider)
		{
            _serviceProvider = serviceProvider;

            var scope = _serviceProvider.CreateScope();
            _workerService = scope.ServiceProvider.GetService<WorkerService>();
            _watcherService = scope.ServiceProvider.GetService<IWatcherService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _workerService.ExecuteAsync(WorkerType.WatcherSendMessage,
                                                  (worker) => _watcherService.SendMessagesAsync(worker),
                                                  stoppingToken);
            }
        }
    }
}

