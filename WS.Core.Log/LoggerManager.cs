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
        /// <summary>
        /// 删除日志文件
        /// </summary>
        /// <param name="days"></param>
        /// <param name="logFolder"></param>
        /// <param name="clearLogger"></param>
        public static void DeleteLogs(int days, string logFolder, ILogger clearLogger) { }
        /// <summary>
        /// 取消日志事件
        /// </summary>
        /// <param name="loggerName"></param>
        /// <param name="logLevel"></param>
        public static void DisableLogEvent(string loggerName, LogLevels logLevel) { }
        /// <summary>
        /// 取消日志器
        /// </summary>
        /// <param name="loggerName"></param>
        /// <param name="logLevel"></param>
        public static void DisableLogger(string loggerName, LogLevels logLevel) { }
        /// <summary>
        /// 取消日志记录
        /// </summary>
        public static void DisableLogging() { }
        /// <summary>
        /// 开始日志事件
        /// </summary>
        /// <param name="loggerName"></param>
        /// <param name="logLevel"></param>
        public static void EnableLogEvent(string loggerName, LogLevels logLevel) { }
        /// <summary>
        /// 开始日志器
        /// </summary>
        /// <param name="loggerName"></param>
        /// <param name="logLevel"></param>
        public static void EnableLogger(string loggerName, LogLevels logLevel) { }
        /// <summary>
        /// 开始日志记录
        /// </summary>
        public static void EnableLogging() { }
        /// <summary>
        /// 获取日志器
        /// </summary>
        /// <param name="loggerName"></param>
        /// <returns></returns>
        public static ILogger GetLogger(string loggerName)
        {
            return new Logger(new LoggerConfig
            {
                LoggerRoot = "./log/"+loggerName,
                LoggerName = loggerName,
                FileFormat = loggerName,
                TimeFormat = "yyyy-MM-dd HH:mm:ss.SSSSSSK"
            });
        }
        /// <summary>
        /// 初始化日志器
        /// </summary>
        /// <param name="config"></param>
        public static void InitLogger(LogConfig config) { }
        //public static void MapLogger(string loggerName, LogLevels logLevel, string logFileName, Layout layout = null) { }
        /// <summary>
        /// 设置日志层级
        /// </summary>
        /// <param name="logLevel"></param>
        public static void SetLoggerAboveLevels(LogLevels logLevel) { }
        /// <summary>
        /// 设置日志层级
        /// </summary>
        /// <param name="logLevels"></param>
        public static void SetLoggerLevel(LogLevels logLevels) { }
        /// <summary>
        /// 清理日志
        /// </summary>
        /// <param name="days"></param>
        /// <param name="logFolder"></param>
        /// <param name="clearLogger"></param>
        public static void StartClear(int days, string logFolder, ILogger clearLogger) { }
    }
}
