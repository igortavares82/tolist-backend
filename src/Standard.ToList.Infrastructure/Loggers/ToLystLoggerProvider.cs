using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Standard.ToList.Model.Aggregates;
using Standard.ToList.Model.Aggregates.Configuration;
using Standard.ToList.Model.Aggregates.Logs;
using Standard.ToList.Model.Options;

namespace Standard.ToList.Infrastructure
{
    public sealed class ToLystLoggerProvider : ILoggerProvider
    {
        private readonly IDisposable? _onChangeToken;
        private readonly IRepository<Log> _repository;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IMemoryCache _memoryCache;
        private LoggerOptions _currentConfig;
        private readonly ConcurrentDictionary<string, ToLystLogger> _loggers = new ConcurrentDictionary<string, ToLystLogger>();

        public ToLystLoggerProvider(IOptionsMonitor<LoggerOptions> config, IServiceProvider serviceProvider)
        {
            _currentConfig = config.CurrentValue;

            _repository = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IRepository<Log>>();
            _configurationRepository = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IConfigurationRepository>();
            _memoryCache = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IMemoryCache>();
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        }

        public ILogger CreateLogger(string categoryName) => 
            _loggers.GetOrAdd(categoryName, name => new ToLystLogger(name, GetCurrentConfig, _repository, _configurationRepository, _memoryCache));

        private LoggerOptions GetCurrentConfig() => _currentConfig;

        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken?.Dispose();
        }
    }
}
