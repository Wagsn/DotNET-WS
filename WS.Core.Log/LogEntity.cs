using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Core.Log
{
    /// <summary>
    /// 一条日志的实体
    /// </summary>
    public class LogEntity
    {
        public LogEntity() { }

        /// <summary>
        /// 哪个日志器，LoggerManager.GetLogger(string LogName)
        /// </summary>
        public string Logger { get; set; }
        /// <summary>
        /// 日志打印的时间
        /// </summary>
        public DateTime LogTime { get; set; }
        /// <summary>
        /// 日志正文
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 日志层级
        /// </summary>
        public string LogLevel { get; set; }
    }
}
