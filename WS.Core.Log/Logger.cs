using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Core.Log
{
    /// <summary>
    /// 日志器，每个日志器一个配置，还有一个总配置
    /// </summary>
    class Logger : ILogger
    {
        public string LoggerName { get; set; }

        public Logger(string loggerName)
        {
            LoggerName = loggerName;
        }

        public void Debug(string message)
        {
            throw new NotImplementedException();
        }

        public void Debug(string formatString, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(string formatString, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string formatString, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void Info(string formatString, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Log(LogLevels logLevel, string message)
        {
            throw new NotImplementedException();
        }

        public void Log(LogLevels logLevel, string formatString, params object[] args)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void Warn(string formatString, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
