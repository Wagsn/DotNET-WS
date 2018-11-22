using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Core.Log
{
    /// <summary>
    /// 日志管理
    /// </summary>
    public class LoggerManager
    {

        public static bool IsLoggingEnabled { get; }

        //public static event EventHandler<LoggingEventArgs> Logging;

        public static void DeleteLogs(int days, string logFolder, ILogger clearLogger) { }
        public static void DisableLogEvent(string loggerName, LogLevels logLevel) { }
        public static void DisableLogger(string loggerName, LogLevels logLevel) { }
        public static void DisableLogging() { }
        public static void EnableLogEvent(string loggerName, LogLevels logLevel) { }
        public static void EnableLogger(string loggerName, LogLevels logLevel) { }
        public static void EnableLogging() { }
        public static ILogger GetLogger(string loggerName)
        {
            return new Logger(loggerName);
        }
        public static void InitLogger(LogConfig config) { }
        //public static void MapLogger(string loggerName, LogLevels logLevel, string logFileName, Layout layout = null) { }
        public static void SetLoggerAboveLevels(LogLevels logLevel) { }
        public static void SetLoggerLevel(LogLevels logLevels) { }
        public static void StartClear(int days, string logFolder, ILogger clearLogger) { }
    }
}
