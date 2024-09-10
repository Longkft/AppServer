﻿using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Logging
{
    public class GameLogger : IGameLogger
    {
        private readonly ILogger _logger;

        public GameLogger()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File("logging/log-.txt", LogEventLevel.Error, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
        public void Error(string error, Exception exception)
        {
            _logger.Error(error, exception);
        }

        public void Error(string error)
        {
            _logger.Error(error);
        }

        public void Info(string info)
        {
            _logger.Information($"{info}");
        }

        public void Print(string msg)
        {
            _logger.Information($">>>>>> {msg}");
        }

        public void Warning(string ms, Exception exception = null)
        {
            _logger.Warning(ms, exception);
        }
    }
}
