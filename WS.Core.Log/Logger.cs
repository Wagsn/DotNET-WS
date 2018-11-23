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
            Console.WriteLine(message);
            // save to file
            File.WriteAllText(Config.LoggerRoot +"/"+ Config.FileFormat + ".log", message);
        }

        public void Debug(string formatString, params object[] args)
        {
            Console.WriteLine(formatString, args);
        }

        public void Error(string message)
        {
            Console.WriteLine(message);
        }

        public void Error(string formatString, params object[] args)
        {
            Console.WriteLine(formatString, args);
        }

        public void Fatal(string message)
        {
            Console.WriteLine(message);
        }

        public void Fatal(string formatString, params object[] args)
        {
            Console.WriteLine(formatString, args);
        }

        public void Info(string message)
        {
            Console.WriteLine(message);
        }

        public void Info(string formatString, params object[] args)
        {
            Console.WriteLine(formatString, args);
        }

        public void Log(LogLevels logLevel, string message)
        {
            Console.WriteLine(message);
        }

        public void Log(LogLevels logLevel, string formatString, params object[] args)
        {
            Console.WriteLine(formatString, args);
        }

        public void Trace(string message)
        {
            Console.WriteLine(message);
        }

        public void Trace(string formatString, params object[] args)
        {
            Console.WriteLine(formatString, args);
        }

        public void Warn(string message)
        {
            Console.WriteLine(message);
        }

        public void Warn(string formatString, params object[] args)
        {
            Console.WriteLine(formatString, args);
        }

        /// <summary>
        /// 记录
        /// </summary>
        /// <param name="config">记录配置（文件保存路径）</param>
        /// <param name="entity">记录实体（记录包含信息）</param>
        public static void Log(LoggerConfig config,  LogEntity entity)
        {
            // log item: [timeformat] [level] [name] [message]
            string logItem = $"[{entity.LogTime.ToString(config.TimeFormat)}] [{entity.LogLevel.ToString()}] [{entity.LogName}] {entity.Message}";
            File.WriteAllText(config.LoggerRoot + "/" + config.LoggerName + ".log", logItem, true);
        }
    }
}
