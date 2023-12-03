using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Standard.ToLyst.Model.Aggregates;
using Standard.ToLyst.Model.Aggregates.Configuration;
using Standard.ToLyst.Model.Aggregates.Logs;
using Standard.ToLyst.Model.Options;

namespace Standard.ToLyst.Infrastructure
{
    public class ToLystLogger : ILogger
    {
        private readonly string _name;
        private readonly Func<LoggerOptions> _getCurrentConfig;
        private readonly IRepository<Log> _repository;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IServiceProvider _serviceProvider;

        public ToLystLogger(string name, 
                            Func<LoggerOptions> getCurrentConfig, 
                            IRepository<Log> repository, 
                            IConfigurationRepository configurationRepository, 
                            IServiceProvider serviceProvider)
        {
            _name = name;
            _getCurrentConfig = getCurrentConfig;
            _repository = repository;
            _configurationRepository = configurationRepository;
            _serviceProvider = serviceProvider;  
        }

        public IDisposable BeginScope<TState>(TState state) where TState : notnull => default!;

        public bool IsEnabled(LogLevel logLevel) 
        {
            var cache = _serviceProvider.GetRequiredService<IMemoryCache>();
            var cacheName = GetType().FullName.ToString();
            var isEnabled = false;
            Logger loggerConfiguration = null;

            cache.TryGetValue(cacheName, out loggerConfiguration);

            if (loggerConfiguration == null) 
            {
                var configuration = _configurationRepository.GetOneAsync(it => it.IsEnabled == true).Result;
                loggerConfiguration = configuration?.Logger;
                cache.Set(cacheName, loggerConfiguration, TimeSpan.FromSeconds(1));
            }

            var levelConfig = loggerConfiguration?.LevelConfiguration.Where(it => it.Key == logLevel && it.Value == true);
            return levelConfig != null ? levelConfig.Any() : false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            LoggerOptions config = _getCurrentConfig();

            if (config.EventId == 0 || config.EventId == eventId.Id)
            {
                var log = new Log(_name, DateTime.Now, logLevel, $"{formatter(state, exception)}", exception);
                log.SetExpireAt(config.ExpirationTime);

                Task.Run(() => _repository.CreateAsync(log));
            }
        }
    }
}
