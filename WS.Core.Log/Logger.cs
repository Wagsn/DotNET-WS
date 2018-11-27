using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.Core.IO;

namespace WS.Core.Log
{
    /// <summary>
    /// 日志器，每个日志器一个配置，还有一个总配置
    /// </summary>
    class Logger : ILogger
    {
        /// <summary>
        /// 配置文件
        /// </summary>
        public LoggerConfig Config { get; set; }
        
        /// <summary>
        /// 配置文件包含日志器名以及文件位置和日志格式等内容
        /// </summary>
        /// <param name="config"></param>
        public Logger(LoggerConfig config)
        {
            Config = config;
        }

        public void Debug(string message)
        {
            Log(Config, LogLevels.Debug, message);
        }

        public void Debug(string formatString, params object[] args)
        {
            Log(Config, LogLevels.Debug, formatString, args);
        }

        public void Error(string message)
        {
            Log(Config, LogLevels.Error, message);
        }

        public void Error(string formatString, params object[] args)
        {

            Log(Config, LogLevels.Error, formatString, args);
        }

        public void Fatal(string message)
        {
            Log(Config, LogLevels.Fatal, message);
        }

        public void Fatal(string formatString, params object[] args)
        {
            Log(Config, LogLevels.Fatal, formatString, args);
        }

        public void Info(string message)
        {
            Log(Config, LogLevels.Info, message);
        }

        public void Info(string formatString, params object[] args)
        {
            Log(Config, LogLevels.Info, formatString, args);
        }

        public void Log(LogLevels logLevel, string message)
        {
            Log(Config, logLevel, message);
        }

        public void Log(LogLevels logLevel, string formatString, params object[] args)
        {
            Log(Config, logLevel, formatString, args);
        }

        public void Trace(string message)
        {
            Log(Config, LogLevels.Trace, message);
        }

        public void Trace(string formatString, params object[] args)
        {
            Log(Config, LogLevels.Trace, formatString, args);
        }

        public void Warn(string message)
        {
            Log(Config, LogLevels.Warn, message);
        }

        public void Warn(string formatString, params object[] args)
        {
            Log(Config, LogLevels.Warn, formatString, args);
        }

        /// <summary>
        /// 带格式化的
        /// </summary>
        /// <param name="config">日志器配置文件</param>
        /// <param name="level">日志层级</param>
        /// <param name="formatString">模板字符串</param>
        /// <param name="args">填充字符串数组</param>
        public static void Log(LoggerConfig config, LogLevels level, string formatString, params object[] args)
        {
            Log(config, level, string.Format(formatString, args));
        }

        /// <summary>
        /// 不带格式化的
        /// </summary>
        /// <param name="config">日志器配置文件</param>
        /// <param name="level">日志层级</param>
        /// <param name="message">日志正文</param>
        public static void Log(LoggerConfig config, LogLevels level, string message)
        {
            Log(config, new LogEntity
            {
                LogLevel = level,
                LogName = config.LoggerName,
                LogTime = DateTime.Now,
                Message = message
            });
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="config">记录配置（文件保存路径）</param>
        /// <param name="entity">记录实体（记录包含信息）</param>
        public static void Log(LoggerConfig config,  LogEntity entity)
        {
            //string consoleInfo = $"[{entity.LogLevel.ToString()}] [{entity.LogName}] {entity.Message}";
            string logItem = $"[{entity.LogTime.ToString(config.TimeFormat)}] [{entity.LogLevel.ToString()}] [{entity.LogName}] {entity.Message}";
            Console.WriteLine(logItem);
            File.WriteAllText(config.LoggerRoot + "/" + DateTime.Now.ToString(config.FileFormat) + ".log", logItem+"\r\n", true);
        }
    }
}
