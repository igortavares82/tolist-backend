using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Standard.ToList.Model.Aggregates;
using Standard.ToList.Model.Aggregates.Configuration;
using Standard.ToList.Model.Aggregates.Logs;
using Standard.ToList.Model.Options;

namespace Standard.ToList.Infrastructure
{
    public class ToLystLogger : ILogger
    {
        private readonly string _name;
        private readonly Func<LoggerOptions> _getCurrentConfig;
        private readonly IRepository<Log> _repository;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IMemoryCache _memoryCache;

        public ToLystLogger(string name, 
                            Func<LoggerOptions> getCurrentConfig, 
                            IRepository<Log> repository,
                            IConfigurationRepository configurationRepository,
                            IMemoryCache memoryCache) =>
            (_name, _getCurrentConfig, _repository, _configurationRepository, _memoryCache) = 
            (name, getCurrentConfig, repository, configurationRepository, memoryCache);

        public IDisposable BeginScope<TState>(TState state) where TState : notnull => default!;

        public bool IsEnabled(LogLevel logLevel) 
        {
            var cacheName = this.GetType().FullName.ToString();
            var isEnabled = false;
            _memoryCache.TryGetValue(cacheName, out Logger loggerConfiguration);

            if (loggerConfiguration == null) {
                var configuration = _configurationRepository.GetOneAsync(it => it.IsEnabled == true).Result;
                loggerConfiguration = configuration!.Logger;
                _memoryCache.Set(cacheName, loggerConfiguration, TimeSpan.FromSeconds(3));
            }

            return loggerConfiguration.LevelConfiguration.FirstOrDefault(it => it.Key == logLevel)!.Value;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            LoggerOptions config = _getCurrentConfig();

            if (config.EventId == 0 || config.EventId == eventId.Id)
            {
                var log = new Log(_name, DateTime.Now, logLevel, $"{formatter(state, exception)}", exception);
                Task.Run(() => _repository.CreateAsync(log));
            }
        }
    }
}
