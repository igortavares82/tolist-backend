using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Standard.ToLyst.Model.Aggregates;
using Standard.ToLyst.Model.Aggregates.Configuration;
using Standard.ToLyst.Model.Aggregates.Logs;
using Standard.ToLyst.Model.Options;

namespace Standard.ToLyst.Infrastructure
{
    public sealed class ToLystLoggerProvider : ILoggerProvider
    {
        private readonly IDisposable? _onChangeToken;
        private readonly IRepository<Log> _repository;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly  IServiceProvider _serviceProvider;
        private LoggerOptions _currentConfig;
        private readonly ConcurrentDictionary<string, ToLystLogger> _loggers = new ConcurrentDictionary<string, ToLystLogger>();

        public ToLystLoggerProvider(IOptionsMonitor<LoggerOptions> config, IServiceProvider serviceProvider)
        {
            var provider =  serviceProvider.CreateScope().ServiceProvider;
            
            _currentConfig = config.CurrentValue;
            _repository = provider.GetRequiredService<IRepository<Log>>();
            _configurationRepository = provider.GetRequiredService<IConfigurationRepository>();
            _serviceProvider = serviceProvider;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        }

        public ILogger CreateLogger(string categoryName) => 
            _loggers.GetOrAdd(categoryName, name => new ToLystLogger(name, GetCurrentConfig, _repository, _configurationRepository, _serviceProvider));

        private LoggerOptions GetCurrentConfig() => _currentConfig;

        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken?.Dispose();
        }
    }
}
