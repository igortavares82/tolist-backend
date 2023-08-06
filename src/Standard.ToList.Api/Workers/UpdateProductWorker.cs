using Microsoft.Extensions.Options;
using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.Options;

namespace Standard.ToList.Api.Workers
{
    public class UpdateProductWorker : BackgroundService
	{
        private readonly IMarketService _marketGateway;
        private readonly AppSettingOptions _settings;
        private readonly IServiceProvider _serviceProvider;
        private readonly int _delay = -1;

        public UpdateProductWorker(IServiceProvider serviceProvider)
		{
            _serviceProvider = serviceProvider;

            var scope = _serviceProvider.CreateScope();

            _marketGateway = scope.ServiceProvider.GetService<IMarketService>();
            _settings = scope.ServiceProvider.GetService<IOptions<AppSettingOptions>>().Value;
            _delay = _settings.Workers.MarketWorker.UpdateProductsDelay;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _marketGateway.SearchOutdatedProductsAsync();
                }
                catch (Exception ex)
                {

                }

                await Task.Delay(_delay, stoppingToken);
            }
        }
    }
}
