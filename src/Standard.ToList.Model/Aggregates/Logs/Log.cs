using System;
using Microsoft.Extensions.Logging;
using Standard.ToList.Model.SeedWork;


namespace Standard.ToList.Model.Aggregates.Logs
{
    public class Log : Entity, IAggregateRoot
    {
        public Log(string name, DateTime date, LogLevel logLevel, string message)
        {
            Name = name;
            Date = date;
            LogLevel = logLevel;
            Message = message;
        }

        public Log(string name, DateTime date, LogLevel logLevel, string message, Exception? exception) : this(name, date, logLevel, message)
        {
            Exception = exception;
        }

        public string Name { get; set; }
        public DateTime Date { get; set; }
        public LogLevel LogLevel { get; set; }
        public string Message { get; set; }
        public Exception? Exception { get; set; }
    }
}
