using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.Options;

namespace Standard.ToList.Api.Workers
{
	public class MarketWorker : BackgroundService
	{
        private readonly IMarketService _marketGateway;
        private readonly AppSettingOptions _settings;
        private readonly IServiceProvider _serviceProvider;

        public MarketWorker(IServiceProvider serviceProvider)
		{
            _serviceProvider = serviceProvider;

            var scope = _serviceProvider.CreateScope();

            _marketGateway = scope.ServiceProvider.GetService<IMarketService>();
            _settings = scope.ServiceProvider.GetService<IOptions<AppSettingOptions>>().Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _marketGateway.SearchMissingProductsAsync();
                await Task.Delay(_settings.Workers.MarketWorker.Delay, stoppingToken);
            }
        }
    }
}
