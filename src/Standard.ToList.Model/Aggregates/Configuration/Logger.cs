using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Standard.ToList.Model
{
    public class Logger
    {
        public Dictionary<LogLevel, bool> LogFlags { get; set; } = new Dictionary<LogLevel, bool>()
        {
            [LogLevel.Trace] = false,
            [LogLevel.Debug] = true,
            [LogLevel.Information] = true,
            [LogLevel.Warning] = false,
            [LogLevel.Error] = false,
        };
    }
}
