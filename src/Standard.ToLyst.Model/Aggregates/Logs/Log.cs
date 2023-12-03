using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
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
            if (Exceptions == null && exception != null) 
            {
                Exceptions = new List<Tuple<string, string>>();
            }

            var _exception = exception;
            while(_exception != null)
            {
                Exceptions.Add(new Tuple<string, string>(_exception.Message, _exception.StackTrace));
                _exception = _exception.InnerException;
            }
        }

        public string Name { get; set; }
        public DateTime Date { get; set; }
        public LogLevel LogLevel { get; set; }
        public string Message { get; set; }

        [BsonRepresentation(BsonType.String)]
        List<Tuple<string, string>> Exceptions { get; set; }
    }
}
