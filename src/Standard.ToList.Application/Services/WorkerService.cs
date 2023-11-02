using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Standard.ToList.Model.Aggregates.Configuration;

namespace Standard.ToList.Application.Services
{
    public class WorkerService
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IServiceProvider _serviceProvider;
        private readonly int _delay = 1000;

        public WorkerService(IServiceProvider serviceProvider)
		{
            _serviceProvider = serviceProvider;

            var scope = _serviceProvider.CreateScope();
            _configurationRepository = scope.ServiceProvider.GetService<IConfigurationRepository>();
        }

		public async Task ExecuteAsync(WorkerType workerType, Action<Worker> action, CancellationToken stoppingToken)
		{
            var configuration = await _configurationRepository.GetOneAsync(it => it.Workers.Length > 0);
            var worker = configuration?.Workers.FirstOrDefault(it => it.Type == workerType);

            if (worker == null || worker.IsEnabled == false)
            {
                await Task.Delay(_delay, stoppingToken);
                return;
            }

            worker.Start();

            await Task.Run(() => action(worker));

            worker.End();

            await _configurationRepository.UpdateAsync(it => it.Id == configuration.Id, configuration);
            await Task.Delay(worker.Delay, stoppingToken);
        }
	}
}

