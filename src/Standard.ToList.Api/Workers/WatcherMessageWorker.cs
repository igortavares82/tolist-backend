using Microsoft.Extensions.Options;
using Standard.ToList.Model.Aggregates.Watchers;
using Standard.ToList.Model.Options;

namespace Standard.ToList.Api.Workers
{
    public class WatcherMessageWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IWatcherService _watcherService;
        private readonly AppSettingOptions _settings;
        private readonly int _delay = -1;

        public WatcherMessageWorker(IServiceProvider serviceProvider)
		{
            _serviceProvider = serviceProvider;

            var scope = _serviceProvider.CreateScope();

            _watcherService = scope.ServiceProvider.GetService<IWatcherService>();
            _settings = scope.ServiceProvider.GetService<IOptions<AppSettingOptions>>().Value;
            _delay = _settings.Workers.WatcherWorker.WatcherDelay;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _watcherService.SendMessagesAsync();
                }
                catch (Exception ex)
                {

                }

                await Task.Delay(_delay, stoppingToken);
            }
        }
    }
}

