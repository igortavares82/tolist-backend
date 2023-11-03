using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Standard.ToList.Model.Aggregates;
using Standard.ToList.Model.Aggregates.Logs;
using Standard.ToList.Model.Options;

namespace Standard.ToList.Infrastructure
{
    public sealed class ToLystLoggerProvider : ILoggerProvider
    {
        private readonly IDisposable? _onChangeToken;
        private readonly IRepository<Log> _repository;
        private LoggerOptions _currentConfig;
        private readonly ConcurrentDictionary<string, ToLystLogger> _loggers = new ConcurrentDictionary<string, ToLystLogger>();

        public ToLystLoggerProvider(IOptionsMonitor<LoggerOptions> config, IServiceProvider serviceProvider)
        {
            _currentConfig = config.CurrentValue;

            _repository = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IRepository<Log>>();
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        }

        public ILogger CreateLogger(string categoryName) => _loggers.GetOrAdd(categoryName, name => new ToLystLogger(name, GetCurrentConfig, _repository));

        private LoggerOptions GetCurrentConfig() => _currentConfig;

        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken?.Dispose();
        }
    }
}
