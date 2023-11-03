using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Standard.ToList.Model.Aggregates;
using Standard.ToList.Model.Aggregates.Logs;
using Standard.ToList.Model.Options;

namespace Standard.ToList.Infrastructure
{
    public class ToLystLogger : ILogger
    {
        private readonly string _name;
        private readonly Func<LoggerOptions> _getCurrentConfig;
        private readonly IRepository<Log> _repository;

        public ToLystLogger(string name, Func<LoggerOptions> getCurrentConfig, IRepository<Log> repository) =>
            (_name, _getCurrentConfig, _repository) = (name, getCurrentConfig, repository);

        public IDisposable BeginScope<TState>(TState state) where TState : notnull => default!;

        public bool IsEnabled(LogLevel logLevel) => _getCurrentConfig().LogFlags.FirstOrDefault(it => it.Key == logLevel)!.Value;

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
