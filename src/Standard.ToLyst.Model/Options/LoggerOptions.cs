using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Standard.ToLyst.Model.Options
{
    public class LoggerOptions
    {
        public int EventId { get; set; }
        public double ExpirationTime { get; set; } = 604800;

        public Dictionary<LogLevel, bool> LogFlags { get; set; } = new Dictionary<LogLevel, bool>()
        {
            [LogLevel.Information] = true
        };
    }
}
