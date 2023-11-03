using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Standard.ToList.Model.Options
{
    public class LoggerOptions
    {
        public int EventId { get; set; }

        public Dictionary<LogLevel, bool> LogFlags { get; set; } = new Dictionary<LogLevel, bool>()
        {
            [LogLevel.Information] = true
        };
    }
}
