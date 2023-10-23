using Microsoft.Extensions.Options;
using Standard.ToList.Model.Aggregates.Configuration;
using Standard.ToList.Model.Aggregates.Watchers;
using Standard.ToList.Model.Options;

namespace Standard.ToList.Api.Workers
{
    public class WatcherMessageWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IWatcherService _watcherService;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly AppSettingOptions _settings;
        private readonly int _delay = -1;

        public WatcherMessageWorker(IServiceProvider serviceProvider)
		{
            _serviceProvider = serviceProvider;

            var scope = _serviceProvider.CreateScope();

            _watcherService = scope.ServiceProvider.GetService<IWatcherService>();
            _settings = scope.ServiceProvider.GetService<IOptions<AppSettingOptions>>().Value;
            _configurationRepository = scope.ServiceProvider.GetService<IConfigurationRepository>();
            _delay = _settings.Workers.WatcherWorker.WatcherDelay;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var configuration = await _configurationRepository.GetOneAsync(it => it.Workers.Length > 0);
                    var worker = configuration?.Workers.First(it => it.Type == WorkerType.Watcher);

                    if (worker == null)
                        continue;

                    _watcherService.SendMessagesAsync();
                    _watcherService.UpdateWatchersAsync();
                }
                catch (Exception ex)
                {

                }

                await Task.Delay(_delay, stoppingToken);
            }
        }
    }
}

