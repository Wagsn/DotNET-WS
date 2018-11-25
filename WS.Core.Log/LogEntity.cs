﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Core.Log
{
    /// <summary>
    /// 一条日志的实体，将会输出到日志文件中
    /// LogName
    /// LogTime
    /// Message
    /// LogLevel
    /// </summary>
    public class LogEntity
    {
        /// <summary>
        /// [2018-11-22 17:31.421428+8:00] [Trace] [LogName] Message
        /// </summary>
        public LogEntity() { }

        /// <summary>
        /// 哪个日志器，LoggerManager.GetLogger(string LoggerName)
        /// </summary>
        public string LogName { get; set; }
        /// <summary>
        /// 日志打印的时间
        /// </summary>
        public DateTime LogTime { get; set; }
        /// <summary>
        /// 日志正文
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 日志层级，Trace Debug Info Warn Fatal All
        /// </summary>
        public string LogLevel { get; set; }
    }
}