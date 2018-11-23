using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Core.Log
{
    /// <summary>
    /// 日志配置文件
    /// </summary>
    public class LogConfig
    {
        public LogConfig() { }

        /// <summary>
        /// 文件模板
        /// </summary>
        public string LogFileTemplate { get; set; }
        /// <summary>
        /// 日志模板
        /// </summary>
        public string LogContentTemplate { get; set; }
        /// <summary>
        /// 是否异步
        /// </summary>
        public bool IsAsync { get; set; }
        /// <summary>
        /// 日志根路径（默认./log）
        /// </summary>
        public string LogBaseDir { get; set; }
        public LogLevels LogLevels { get; set; }
        public int MaxArchiveFiles { get; set; }
        public string MaxFileSize { get; set; }
        public string ArchiveFileTemplate { get; set; }
        public string ArchiveBaseDir { get; set; }
        public string ArchiveWriteMode { get; set; }
        public string ArchiveEvery { get; set; }
        public bool? ZipArchiveFile { get; set; }

        /// <summary>
        /// 日志根路径
        /// </summary>
        public string LogRoot { get; set; }

        /// <summary>
        /// 错误输出路径
        /// </summary>
        public string ErrRoot { get; set; }

        /// <summary>
        /// 调试输出路径
        /// </summary>
        public string DebugRoot { get; set; }

        /// <summary>
        /// 时间格式
        /// </summary>
        public string TimeFormat { get; set; }
    }
}
