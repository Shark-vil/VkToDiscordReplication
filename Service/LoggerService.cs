using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkToDiscordReplication.Service
{
    internal class LoggerService
    {
        private static ILoggerFactory? _loggerFactory = null;

        private static void MakeLoggerFactory()
        {
            if (_loggerFactory != null)
                return;

#if DEBUG
            LogLevel logLevel = LogLevel.Debug;
#else
            LogLevel logLevel = LogLevel.Information;
#endif

            _loggerFactory = LoggerFactory.Create(builder => builder
                .SetMinimumLevel(logLevel)
                .AddSimpleConsole(c => {
                    c.TimestampFormat = "[HH:mm:ss] ";
                })
            );
        }

        internal static ILogger<T> GetLogger<T>()
        {
            MakeLoggerFactory();

            if (_loggerFactory == null)
                throw new NullReferenceException(nameof(_loggerFactory));

            ILogger<T> logger = _loggerFactory.CreateLogger<T>();
            return logger;
        }

        internal static ILogger GetLogger(string loggerName)
        {
            MakeLoggerFactory();

            if (_loggerFactory == null)
                throw new NullReferenceException(nameof(_loggerFactory));

            ILogger logger = _loggerFactory.CreateLogger(loggerName);
            return logger;
        }
    }
}
