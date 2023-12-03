using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Standard.ToLyst.Application
{
    public class LoggerCommand
    {
        public Dictionary<LogLevel, bool> LevelConfiguration { get; set; } = new Dictionary<LogLevel, bool>()
        {
            [LogLevel.Trace] = false,
            [LogLevel.Debug] = true,
            [LogLevel.Information] = true,
            [LogLevel.Warning] = false,
            [LogLevel.Error] = false,
        };
    }
}
